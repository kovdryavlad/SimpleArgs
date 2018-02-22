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

            var flags           = StaticFields.Where(f => f.FieldType == typeof(ArgFlag)).ToArray();
            var argsParamINT    = StaticFields.Where(f => f.FieldType == typeof(ArgParam<int>)).ToArray();
            var argsParamDouble = StaticFields.Where(f => f.FieldType == typeof(ArgParam<double>)).ToArray();
            var argsParamString = StaticFields.Where(f => f.FieldType == typeof(ArgParam<string>)).ToArray();

            var service = StaticFields.Where(f => f.FieldType == typeof(ArgService)).ToArray();

            GetFlags(args, flags.Select(f => (ArgFlag)f.GetValue(null)).ToArray(), (ArgService)service[0].GetValue(null));
            //GetParams(args, )
        }

        public static void GetFlags(string[] args, ArgFlag[] flags, ArgService service)
        {
            ArgFlag help = new ArgFlag("?", "help", "Подсказка программы");

            List<int> indexesToDel = new List<int>();

            for (int i = 0; i < args.Length; i++)
            {
                for (int j = 0; j < flags.Length; j++)
                    if (Compare(args[i], flags[j]))
                    {
                        flags[j].WasInitialized = true;
                        indexesToDel.Add(i);
                    }
                if (Compare(args[i], help))
                    HelpOutput(service);
            }

            DeleteFlagsFromArgs(args, indexesToDel);
        }

       
        public static bool Compare(string arg, ArgFlag param)
        {
            if (arg == "-" + param.Key || arg == "/" + param.Key || arg == "-" + param.LongKey || arg == "/" + param.LongKey)
                return true;

            return false;
        }

        private static void HelpOutput(ArgService service)
        {
            //TODO - написать вывод справки
            Console.Error.WriteLine();
            Environment.Exit(1);
            throw new NotImplementedException();
        }

        private static void DeleteFlagsFromArgs(string[] args, List<int> indexesToDel)
        {
            List<string> NewArgs = new List<string>();

            var length = args.Length;
            for (int i = 0; i < length; i++)
                if (!indexesToDel.Contains(i))
                    NewArgs.Add(args[i]);

            args = NewArgs.ToArray();
        }

    }
}
