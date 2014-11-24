﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WhiskeyEditor.Backend.Managers;

namespace WhiskeyEditor.Backend
{
    [Serializable]
    public class CodeDescriptor : FileDescriptor
    {

        public CodeDescriptor(string name)
            : base(ProjectManager.Instance.ActiveProject.PathSrc + Path.DirectorySeparatorChar + name + ".cs", name)
        {

        }

        public String QualifiedName
        {
            get
            {
                return CodeNameSpace + "." + Name;
            }
        }

        protected virtual String CodeNameSpace
        {
            get { return "Project"; }
        }

        protected virtual String CodeClassDef
        {
            get
            {
                return "[Serializable] " + Environment.NewLine + "\tpublic class " + Name;
            }
        }
        protected virtual String[] CodeUsingStatements
        {
            get
            {
                return new string[] { "System", "Whiskey2D.Core" };
            }
        }

        protected virtual void addSpecializedCode(StreamWriter writer)
        {
            //fill in with subclasses
        }

        protected virtual void processExistingCode(string[] allLines)
        {

        }

        public override void inspectFile()
        {
            string[] allLines = File.ReadAllLines(FilePath);

            foreach (String line in allLines)
            {
                if (line.Contains(" class "))
                {
                    int indexStart = line.IndexOf(" class ") + " class ".Length;
                    int indexEnd = line.IndexOf(" :");
                    string realName = line.Substring(indexStart, indexEnd - indexStart).Trim();
                    Name = realName;
                    break;
                }
            }

            processExistingCode(allLines);
        }

        public override void createFile()
        {
            FileStream fileStream = File.Create(FilePath);
            StreamWriter writer = new StreamWriter(fileStream);
               

            foreach (string usingStatement in CodeUsingStatements)
            {
                writer.WriteLine("using " + usingStatement + ";");
            }
            writer.WriteLine("");
            writer.WriteLine("//auto-generated by Whiskey2D");
            writer.WriteLine("namespace " + CodeNameSpace);
            writer.WriteLine("{");

            writer.WriteLine("\t" + CodeClassDef);
            writer.WriteLine("\t{");

            addSpecializedCode(writer);

            writer.WriteLine("\t}");

            writer.WriteLine("}");

            writer.Flush();
            writer.Close();
            fileStream.Close();

        }

    }



}
