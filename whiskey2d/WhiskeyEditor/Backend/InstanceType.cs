﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace WhiskeyEditor.Backend
{
    [Serializable]
    class InstanceType : TypeVal
    {
        private TypeDescriptor descr;
        private InstanceDescriptor instance;

        public event EventHandler ValueChanged = new EventHandler((s, a) => { });

        //private Insta
        public InstanceType(TypeDescriptor descr, InstanceDescriptor instance)
        {
            this.instance = instance;
            this.descr = descr;
        }


        public string TypeName
        {
            get { return this.descr.ClassName; }
        }

        public object Value
        {
            get
            {
                return this.instance;
            }
            set
            {
                //if (value == null)
                //{
                //    this.instance = null;
                //}
                //else 
                    if (value is InstanceDescriptor)
                {
                    InstanceDescriptor newVal = (InstanceDescriptor)value;
                    if (newVal.TypeDescriptorInFileManager.ClassName.Equals(TypeName))
                    {
                        this.instance = newVal;

                    }
                    else throw new WhiskeyException("Given Instance is of incorrect type: " + newVal.TypeDescriptorInFileManager.ClassName + " versus " + TypeName);
                }
                else throw new WhiskeyException("Given value is not an Instance Descriptor");

                
            }
        }


        public TypeVal clone()
        {
            return new InstanceType(descr, instance);
        }
    }
}
