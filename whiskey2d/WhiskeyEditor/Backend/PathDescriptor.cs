﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskeyEditor.Backend
{
    interface PathDescriptor : Descriptor
    {
        string FilePath { get; }


    }
}
