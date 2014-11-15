﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor.Backend
{
    class PropertyDescriptor 
    {

        private string name;
        private TypeVal typeVal;
        private bool secure;

        public PropertyDescriptor(string name, TypeVal typeVal)
            : this(false, name, typeVal)
        {
        }

        public PropertyDescriptor(bool secure, string name, TypeVal typeVal){
            this.name = name;
            this.typeVal = typeVal;
            this.secure = secure;
        }

        public bool Secure
        {
            get { return secure; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public TypeVal TypeVal
        {
            get { return TypeVal; }
        }

    }
}