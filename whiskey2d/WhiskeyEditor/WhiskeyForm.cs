﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Whiskey2D.Service;
using System.Reflection;

namespace WhiskeyEditor
{
    public partial class WhiskeyForm : Form
    {
        ProjectManager projMan;
        Project project;

        public WhiskeyForm()
        {
            InitializeComponent();

            projMan = new ProjectManager();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult result = this.openProjectDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string path = this.openProjectDialog.InitialDirectory + this.openProjectDialog.FileName;

                project = projMan.loadProject(path);
                projMan.setTreeFor(project, this.directoryTree);
                
            }

          

        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = this.newProjectDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string directory = this.newProjectDialog.FileName;
                string name = directory.Substring(directory.LastIndexOf('\\') + 1);
                //directory = directory.Substring(0, directory.Length - name.Length);

                project = projMan.createNewProject(directory, name);
                projMan.setTreeFor(project, this.directoryTree);
                

            }
            else
            {
                debug.Text = "FILE NOT OKAY";
            }
        }

        private void newProjectDialog_FileOk(object sender, CancelEventArgs e)
        {
           
        }

        private void compileButton_Click(object sender, EventArgs e)
        {
            
            //Compiler.getInstance().compileDirectory(project.NameNoExt, project.Directory + "//Src", "compile-lib\\MonoGame.Framework", "compile-lib\\Whiskey.Core");
            debug.Text = "compile done";
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            
            
            Compiler.getInstance().compileDirectory("Whiskey.TestImpl", "TestImpl", "MonoGame.Framework", "Whiskey.Core");
            //add core to path
            Assembly coreAssmebly = Assembly.LoadFrom("Whiskey.Core.dll");

            //add game data to path
            Assembly gameAssembly = Assembly.LoadFrom("Whiskey.TestImpl.dll");

            debug.Text = "assembly loaded " + gameAssembly.ToString();

            //find gameManager
            Type[] coreTypes = coreAssmebly.GetTypes();
            foreach (Type type in coreTypes)
            {
                if (type.Name.Equals("GameManager"))
                {
                    object gameManager = Activator.CreateInstance(type, gameAssembly);
                    //debug.Text = "found game Manager";
                    gameManager.GetType().GetMethod("go").Invoke(gameManager, new object[] { });
                    break;
                    
                }
                Console.WriteLine(type.Name);
            }

           
        }
    }
}
