﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.UI.Menu;
using WhiskeyEditor.UI.Assets;
using WhiskeyEditor.Backend.Managers;
using System.Windows.Forms;

namespace WhiskeyEditor.Backend.Actions.Impl
{
    class NewFileAction : AbstractAction
    {
        

        public NewFileAction()
            : base("New", AssetManager.ICON_FILE)
        {

        }

        public NewFileAction(string name, System.Drawing.Image image)
            : base(name, image)
        {

        }

        protected virtual void beforeShow(NewFileForm form)
        {

        }
        protected virtual void afterShow(NewFileForm form)
        {
            if (form.DialogResult == DialogResult.OK)
            {
                createFile(form.SelectedText, form.SelectedType, form.SelectedOption);
            }
        }
        protected override void run()
        {
            NewFileForm form = new NewFileForm();

            beforeShow(form);
            form.ShowDialog();

            afterShow(form);
            form.Dispose();

        }

        protected void createFile(String fileName, String fileType, String option)
        {
            switch (fileType)
            {
                case NewFileForm.NEW_TYPE:
                    FileManager.Instance.createNewTypeDescriptor(fileName);
                    break;
                case NewFileForm.NEW_SCRIPT:
                    FileManager.Instance.createNewScriptDescriptor(fileName, option);
                    break;
                case NewFileForm.NEW_LEVEL:
                    FileManager.Instance.createNewLevelDescriptor(fileName);
                    break;
                default:
                    break;
            }
        }

    }
}
