﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using System.Reflection;
using Whiskey2D.Core.Managers;
using Whiskey2D.Core.Managers.Impl;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;


namespace WhiskeyEditor.Backend.Managers
{

    [Serializable]
    class InstanceManager
    {
        private static InstanceManager instance = new InstanceManager();
        public static InstanceManager Instance { get { return instance; } }

        //private List<InstanceDescriptor> iDescs;
        public List<Level> Levels { get; private set; }


        private InstanceManager()
        {
           // iDescs = new List<InstanceDescriptor>();
            Levels = new List<Level>();
          

        }
        public void clearLevels()
        {
            Levels.Clear();
        }
        public void addLevel(Level level)
        {
            Levels.Add(level);
        }

        public void syncTypeToInstances(TypeDescriptor typeDescriptor)
        {
            Levels.ForEach((l) => {
                l.syncTypeToInstances(typeDescriptor);
            });

        }


        //public void addInstance(InstanceDescriptor iDesc)
        //{
        //    iDescs.Add(iDesc);
        //}

        //public void clear()
        //{
        //    iDescs.Clear();
        //}


        //public State getState()
        //{
        //    State state = new State();
        //    state.Name = "test";

        //    foreach (InstanceDescriptor inst in iDescs)
        //    {
        //        state.GameObjects.Add(inst);
        //    }
            
        //    return state;
        //}

        //public void setState(State state)
        //{
        //    foreach (GameObject gob in state.GameObjects)
        //    {
        //        addInstance((InstanceDescriptor)gob);
        //    }
        //}

        /// <summary>
        /// Convert a level that is used in the editor, to a level that can be used
        /// by Whiskey.Core. 
        /// </summary>
        /// <param name="dllPathIn">The path to the GameData.dll, that contains the type information</param>
        /// <param name="level">Some level that contains a set of instance descriptors</param>
        /// <returns>The path to the state file that Whiskey.Core will load</returns>
        public string convertToGobs(string dllPathIn, Level level)
        {
            AppDomain buildDomain = AppDomain.CreateDomain("BuildDomain");


            string statePath = ProjectManager.Instance.ActiveProject.PathBuildStates + Path.DirectorySeparatorChar + level.LevelName + ".state";

            buildDomain.SetData("color", level.BackgroundColor);
            buildDomain.SetData("dllPath", dllPathIn);
            buildDomain.SetData("statePath", statePath);
            buildDomain.SetData("iDescs", level.getInstances());
            buildDomain.SetData("scriptTable", ScriptManager.Instance.getScriptTable());

            try
            {

                buildDomain.DoCallBack(new CrossAppDomainDelegate(() =>
                {
                    try
                    {
                        string appDllPath = (string)AppDomain.CurrentDomain.GetData("dllPath");
                        string appStateName = (string)AppDomain.CurrentDomain.GetData("stateName");
                        string appStatePath = (string)AppDomain.CurrentDomain.GetData("statePath");
                        Color backGroundColor = (Color)AppDomain.CurrentDomain.GetData("color");
                        List<InstanceDescriptor> appIDescs = (List<InstanceDescriptor>)AppDomain.CurrentDomain.GetData("iDescs");
                        Dictionary<String, ScriptDescriptor> appScriptTable = (Dictionary<String, ScriptDescriptor>)AppDomain.CurrentDomain.GetData("scriptTable");


                        Assembly gameData = Assembly.LoadFrom(appDllPath);

                        int c = 0;
                        List<GameObject> lGobs = new List<GameObject>();
                        foreach (InstanceDescriptor iDesc in appIDescs)
                        {
                            iDesc.X = iDesc.Position.X;
                            iDesc.Y = iDesc.Position.Y;
                            
                            string typeName = iDesc.TypeDescriptorCompile.QualifiedName;
                            Type type = gameData.GetType(typeName, true, false);

                            GameObject gob = (GameObject)type.GetConstructor
                                (new Type[] {  }).Invoke
                                (new object[] {  });


                            foreach (PropertyDescriptor typeProp in iDesc.TypeDescriptorCompile.getPropertySetClone())
                            {
                                Console.WriteLine ( "converting " + c + " " + typeProp.Name);
                                PropertyInfo propInfo = gob.GetType().GetProperty(typeProp.Name);
                                if (propInfo.SetMethod != null)
                                {
                                    propInfo.GetSetMethod().Invoke(gob, new object[] { iDesc.getTypeValOfName(typeProp.Name).Value });
                                }
                            }

                            gob.clearScripts();

                            foreach (String scriptName in iDesc.getScriptNames())
                            {
                                Type scriptType = gameData.GetType(appScriptTable[scriptName].QualifiedName, false);
                                object script = scriptType.GetConstructor(new Type[] { }).Invoke(new object[] { });
                                gob.addScript((Script)script);
                            }


                            //TODO THIS IS A TEMPORARY WORK AROUND
                            gob.initializeObject();

                            lGobs.Add(gob);
                        }
                        State state = new State();
                        state.GameObjects = lGobs;
                        state.BackgroundColor = backGroundColor;
                        state.Name = appStateName;
                        string filename = State.serialize(
                                            state,
                                            appStatePath);
                        //TempFilePath = filename;

                        //AppDomain.CurrentDomain.SetData("fileName", filename);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        throw e;
                    }
                }));
            
            }
            catch (Exception e)
            {
                Console.WriteLine("broken");
            }
            
            AppDomain.Unload(buildDomain);

            return statePath;
        }

        /*
        public string convertToGobs(string dllPathIn, string stateNameIn)
        {


            AppDomain buildDomain = AppDomain.CreateDomain("BuildDomain");

            
            string statePath = ProjectManager.Instance.ActiveProject.PathBuildStates + Path.DirectorySeparatorChar + stateNameIn;

            buildDomain.SetData("dllPath", dllPathIn);
            buildDomain.SetData("stateName", stateNameIn);
            buildDomain.SetData("statePath", statePath);
            buildDomain.SetData("iDescs", iDescs);
            buildDomain.SetData("scriptTable", ScriptManager.Instance.getScriptTable());

            try
            {

                buildDomain.DoCallBack(new CrossAppDomainDelegate(() =>
                {
                    try
                    {
                        string appDllPath = (string)AppDomain.CurrentDomain.GetData("dllPath");
                        string appStateName = (string)AppDomain.CurrentDomain.GetData("stateName");
                        string appStatePath = (string)AppDomain.CurrentDomain.GetData("statePath");

                        List<InstanceDescriptor> appIDescs = (List<InstanceDescriptor>)AppDomain.CurrentDomain.GetData("iDescs");
                        Dictionary<String, ScriptDescriptor> appScriptTable = (Dictionary<String, ScriptDescriptor>)AppDomain.CurrentDomain.GetData("scriptTable");


                        Assembly gameData = Assembly.LoadFrom(appDllPath);

                        List<GameObject> lGobs = new List<GameObject>();
                        foreach (InstanceDescriptor iDesc in appIDescs)
                        {

                            string typeName = iDesc.TypeDescriptor.QualifiedName;
                            Type type = gameData.GetType(typeName, true, false);

                            GameObject gob = (GameObject)type.GetConstructor
                                (new Type[] { }).Invoke
                                (new object[] { });

                            foreach (PropertyDescriptor typeProp in iDesc.TypeDescriptor.getPropertySetClone())
                            {
                                PropertyInfo propInfo = gob.GetType().GetProperty(typeProp.Name);
                                if (propInfo.SetMethod != null)
                                {
                                    propInfo.GetSetMethod().Invoke(gob, new object[] { iDesc.getTypeValOfName(typeProp.Name).value });
                                }
                            }

                            gob.clearScripts();

                            foreach (String scriptName in iDesc.getScriptNames())
                            {
                                Type scriptType = gameData.GetType(appScriptTable[scriptName].QualifiedName, false);
                                object script = scriptType.GetConstructor(new Type[] { }).Invoke(new object[] { });
                                gob.addScript((Script)script);
                            }


                            //TODO THIS IS A TEMPORARY WORK AROUND
                            gob.initializeObject();

                            lGobs.Add(gob);
                        }
                        State state = new State();
                        state.GameObjects = lGobs;
                        state.Name = appStateName;
                        string filename = State.serialize(
                                            state,
                                            appStatePath);
                        //TempFilePath = filename;

                        AppDomain.CurrentDomain.SetData("fileName", filename);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        throw e;
                    }
                }));

            }
            catch (Exception e)
            {
                Console.WriteLine("broken");
            }
            string fileName = (string)buildDomain.GetData("fileName");

            AppDomain.Unload(buildDomain);

            return fileName;
        }
         * */

    }
}
