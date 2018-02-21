using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace SimpleArgs
{
    public class ParamsHandler
    {
        public static void Handle(string[] args, Type type)
        {
            var StaticFields = type.GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);

            var flags = StaticFields.Where(f => f.FieldType == typeof(ArgFlag)).ToArray();
            var argsParamINT = StaticFields.Where(f => f.FieldType == typeof(ArgParam<int>)).ToArray();
            var argsParamDouble = StaticFields.Where(f => f.FieldType == typeof(ArgParam<double>)).ToArray();
            var argsParamString = StaticFields.Where(f => f.FieldType == typeof(ArgParam<string>)).ToArray();

            var service = StaticFields.Where(f => f.FieldType == typeof(ArgService)).ToArray();
        }
    }
}
