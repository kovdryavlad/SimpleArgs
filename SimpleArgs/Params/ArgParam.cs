using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleArgs
{
    public class ArgParam<T> : ArgFlag, IArgType, IArgParamBase
    {
        public bool ValidateForRequired()
        {
            if (Required && WasInitialized)
                return true;

            return false;
        }

        public Type GetType { get { return typeof(T); } }

        public static implicit operator T(ArgParam<T> p)
        {
            return p.Value;
        }

        public T Value { get { return valueField; } set { valueField = value; WasInitialized = true; } }
        public bool Required { get; set; }
        public string Parametr { get; set; }

        T valueField;

        public ArgParam(string key, string parametr, string description)
             : this(key, null, description, default(T), true, parametr)
        {

        }

        public ArgParam(string key, string longkey, string parametr, string description)
             : this(key, longkey, description, default(T), true, parametr)
        {

        }

        public ArgParam(T value, string key, string parametr, string description)
             : this(key, null, description, value, false, parametr)
        {

        }

        public ArgParam(T value, string key, string longkey, string parametr, string description)
             : this(key, longkey, description, value, false, parametr)
        {

        }

        public ArgParam(string key, string longkey, string description, T value, bool required, string parametr)
            : base(key, longkey, description)
        {
            Value = value;
            Required = required;
            Parametr = parametr;
            WasInitialized = false;
        }
    }
}
