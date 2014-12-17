﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using WhiskeyEditor.UI.Properties.Converters;
using System.Globalization;
using WhiskeyEditor.Backend;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.ComponentModel;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace WhiskeyEditor.UI.Properties
{
    using WhiskeyColor = Whiskey2D.Core.Color;
    class ColorPicker : UITypeEditor
    {

        private ColorDialog colorDialog;
        private IWindowsFormsEditorService service;

        public ColorPicker()
        {
            colorDialog = new ColorDialog();
           

        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            // show the list box
            if (service != null)
            {
                //service.ShowDialog();

                //DialogResult result = service.ShowDialog(colorDialog as Form);
                
                DialogResult result = colorDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Color color = colorDialog.Color;
                    return (WhiskeyColor)UIManager.convertColor(color);
                }

            }

            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }



        public override void PaintValue(PaintValueEventArgs e)
        {

            WhiskeyColor wc = (WhiskeyColor)e.Value;

            using (SolidBrush brush = new SolidBrush(UIManager.convertColor(wc)))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }

            //e.Graphics.DrawRectangleProper(Pens.Black, e.Bounds);
        }
    }
}
