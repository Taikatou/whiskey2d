﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using WhiskeyEditor.Backend.Managers;
using System.CodeDom.Compiler;
using System.IO;

namespace WhiskeyEditor.UI.Output
{
    class OutputView : Control
    {
       

        private DataGridView grid;
        private BindingSource data;
        

        public OutputView()
        {
            this.BackColor = Color.Red;

            this.Size = new Size(100, 100);
            
            

            initControls();
            addControls();

            UIManager.Instance.Compiler.Compiled += new CompileEventHandler(compiledListener);

            

        }

        private void compiledListener(object sender, CompileEventArgs args)
        {

            setErrors(args.Errors);
            

            

        }

        public void setErrors(CompilerErrorCollection errors)
        {
            this.Invoke(new NoArgFunction(() =>
            {
                data.Clear();
                foreach (CompilerError err in errors)
                {

                    string fileName = err.FileName;
                    try
                    {
                        err.FileName = err.FileName.Substring(err.FileName.LastIndexOf(Path.DirectorySeparatorChar));
                    }
                    catch (Exception e)
                    {
                        err.FileName = fileName;
                    }

                    data.Add(err);

                }

            }));
        }

       
        private void initControls()
        {

           

            data = new BindingSource();

            
            grid = new DataGridView();
            grid.Dock = DockStyle.Fill;
            grid.DataSource = data;
            grid.AutoGenerateColumns = false;
            grid.EditMode = DataGridViewEditMode.EditProgrammatically;
            grid.Enabled = false;



            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            
            
            
            column.DataPropertyName = "FileName";
            column.HeaderText = "File";
            column.FillWeight = .3f;
            grid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Line";
            column.HeaderText = "Line";
            column.FillWeight = .1f;
            grid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "ErrorText";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            column.HeaderText = "Error";
            column.FillWeight = .6f;
            grid.Columns.Add(column);

        


           
        }

        private void addControls()
        {
            this.Controls.Add(grid);
            
        }

        
        public void writeException(Exception e)
        {
            if (e == null)
            {
                MessageBox.Show("Unknown Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else
            {
                string err = "ERROR: type{" + e.GetType().Name + "} "
                    + Environment.NewLine + "msg{ " + e.Message + "} "
                    + Environment.NewLine + "src{ " + e.Source + "} "
                    + Environment.NewLine + "trace{ " + e.StackTrace + "} ";
                string msg = "An error occured. If this problem continues, please contact the developers with the information below.";// +Environment.NewLine + err;

                //MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                writeError("Error", msg, err);
            }
        }

        public void writeWarning(WhiskeyEditor.Backend.WhiskeyWarning e)
        {

            if (e == null)
            {
                MessageBox.Show("Unknown Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else
            {
                //string err = e.Warning;
                //string msg = "Warning! " + Environment.NewLine + err;

                //MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                // Get reference to the dialog type.
                writeError("Warning", e.Warning, e.Message);
            }
        }

        public void writeError(string title, string message, string details)
        {
            var dialogTypeName = "System.Windows.Forms.PropertyGridInternal.GridErrorDlg";
            var dialogType = typeof(Form).Assembly.GetType(dialogTypeName);

            // Create dialog instance.
            var dialog = (Form)Activator.CreateInstance(dialogType, new PropertyGrid());

            // Populate relevant properties on the dialog instance.
            dialog.Text = title;
            dialogType.GetProperty("Details").SetValue(dialog, details, null);
            dialogType.GetProperty("Message").SetValue(dialog, message, null);

            // Display dialog.
            var result = dialog.ShowDialog();
        }

        

    }
}
