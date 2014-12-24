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

namespace WhiskeyEditor.UI.Properties
{
    class RestrictedFloatPicker : UITypeEditor
    {

        public float Min { get; set; }
        public float Max { get; set; }
        public float Inc { get; set; }

        public GeneralPropertyDescriptor Property { get; set; }


        private IWindowsFormsEditorService service;
        private TrackBar trackBar;

        private float result = 0;
        public RestrictedFloatPicker(GeneralPropertyDescriptor gpd)
            : this(gpd, 0, 1, .1f)
        {
            
        }
        public RestrictedFloatPicker(GeneralPropertyDescriptor gpd, float min, float max, float inc)
            : this(min, max, inc)
        {
            Property = gpd;
        }
        public RestrictedFloatPicker()
            : this(0, 1, .1f)
        {

        }
        public RestrictedFloatPicker(float min, float max, float inc)
        {
            Min = min;
            Max = max;
            Inc = inc;

            trackBar = new TrackBar();

            //1 - 0 = 1 / .1 = 10
            float range = max - min;

            int maxInt = (int) (range / inc);

            trackBar.Minimum = 0;
            trackBar.Maximum = (int)(range/inc) + 1;
            trackBar.TickFrequency = (int)(range);


            trackBar.ValueChanged += (s, a) =>
            {
                result = Min + (trackBar.Value * Inc);
                if (Property != null)
                    Property.SetValue(null, result);
            };

            trackBar.MouseUp += (s, a) =>
            {
                if (service != null)
                {
                    service.CloseDropDown();
                }
            };

        }


        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            service = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            

            // show
            if (service != null)
            {
                result = (float)value;
                trackBar.Value = (int)(.5f + (result / Inc) );
                service.DropDownControl(trackBar);

                value = result;
            }


            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

    }
}
