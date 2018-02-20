﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleArgs
{
    class ArgParam<T>:ArgFlag
    {
        public T Value { get; set; }
        public bool Required { get; set; }
        public string Parametr { get; private set; }
    }
}
