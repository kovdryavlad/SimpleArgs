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
        static ArgFlag[] AllFlags;
        static List<IArgParamBase> AllParams = new List<IArgParamBase>();
        static List<AlternativeParams> alternativs; 

        public static void Handle(string[] args, Type type)
        {
            var StaticFields = type.GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);

            var service = StaticFields.Where(f => f.FieldType == typeof(ArgService)).ToArray();
            if (service.Length != 1)
                throw new Exception();
            
            var flags = StaticFields.Where(f => f.FieldType == typeof(ArgFlag)).ToArray();
            args = GetFlags(args, flags.Select(f => (ArgFlag)f.GetValue(null)).ToArray(), (ArgService)service[0].GetValue(null));

            var types = TypesInfo.TypeList.Select(t => t.GetType).ToArray();

            var length = types.Length;

            for (int i = 0; i < length; i++)
            {
                Type tp = types[i];
                MethodInfo m = typeof(ParamsHandler).GetMethod("GetParams", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).MakeGenericMethod(new[] { tp });
                args = m.Invoke(null, new object[] { args, StaticFields }) as string[];
            }

            if (args.Length != 0)
            {
                Console.Error.WriteLine("Найдены неожиданные параметры:");
                var lengthNewArgs = args.Length;
                for (int i = 0; i < lengthNewArgs; i++)
                {
                    Console.WriteLine(args[i]);

                }

                Environment.Exit(1);
            }

            //валидация параметров
            ParamsValidation();
                
        }

       

        private static string[] GetParams<T>(string[] args, FieldInfo[] staticFields)
        {
            var argsParams = staticFields.Where(f => f.FieldType == typeof(ArgParam<T>)).Select(f=>(ArgParam<T>)f.GetValue(null)).ToArray();
            Func<string, T> converter = (TypesInfo.TypeList.First(l => l.GetType == typeof(T)) as ArgType<T>).Converter;

            if (argsParams.Length!=0)
                AllParams.AddRange(argsParams);//запоминаем в поле

            var FLength = argsParams.Length;
            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                for (int j = 0; j < FLength; j++)
                {
                    var param = argsParams[j];
                    
                    if (Compare(arg, param)) 
                    {
                            try
                            {
                                param.Value = converter(args[i + 1]);

                                var lst = args.ToList();
                                lst.RemoveAt(i);
                                lst.RemoveAt(i);

                                i--;
                                args = lst.ToArray();
                            }

                            catch(IndexOutOfRangeException ex)
                            {
                                Console.Error.WriteLine("Ключ {0} не имеет значения", args[i]);
                                Environment.Exit(1);
                            }

                            catch
                            {
                                Console.Error.WriteLine("Не удалось преобразовать значение {0} к типу {1}", arg[i+1], typeof(T));
                                Environment.Exit(1);
                            }
                    }
                }


            }

            return args;
        }

        public static string[] GetFlags(string[] args, ArgFlag[] flags, ArgService service)
        {
            //запоминаем в поля
            AllFlags = flags;

            if (service.AlternativesParamsArray != null)
                alternativs = service.AlternativesParamsArray.ToList();

            ArgFlag help = new ArgFlag("?", "help", "Справка программы");

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

            return DeleteFlagsFromArgs(args, indexesToDel);
        }

       
        public static bool Compare(string arg, ArgFlag param)
        {
            if (arg == "-" + param.Key || arg == "/" + param.Key || arg == "-" + param.LongKey || arg == "/" + param.LongKey)
                return true;

            return false;
        }

        private static void HelpOutput(ArgService service)
        {

            var FullAlt = alternativs.Select(a => a.Alternatives).SelectMany(a => a).ToArray();

            Console.Error.WriteLine("Использование:");

            for (int i = 0; i < AllFlags.Length; i++)
            {
                if (!FullAlt.Contains(AllFlags[i]))
                    Console.Write("[-{0}] ", AllFlags[i].Key);

            }

            var NonRequiredAlternatives = alternativs.Where(a => a.Required == false).ToArray();
            for (int i = 0; i < NonRequiredAlternatives.Length; i++)
            {
                Console.Write("[");
                for (int j = 0; j < NonRequiredAlternatives[i].Alternatives.Length; j++)
                {
                    var alt = NonRequiredAlternatives[i].Alternatives[j] as IArgParamBase;

                    if (alt != null)
                    {
                        if (alt.Required)
                        
                            Console.Write(alt.Key + " <" + alt.Parametr +">");
                        else
                            Console.Write("["+alt.Key + " <" + alt.Parametr + ">"+ "]");
                    }
                    else
                        Console.Write("[" + NonRequiredAlternatives[i].Alternatives[j].Key + "]");

                    if (j != NonRequiredAlternatives[i].Alternatives.Length - 1)
                        Console.Write("|");

                }
                Console.Write("] ");
            }

            //TODO - написать вывод справки
            Console.Error.WriteLine();
            Environment.Exit(1);
            throw new NotImplementedException();
        }

        private static string[] DeleteFlagsFromArgs(string[] args, List<int> indexesToDel)
        {
            List<string> NewArgs = new List<string>();

            var length = args.Length;
            for (int i = 0; i < length; i++)
                if (!indexesToDel.Contains(i))
                    NewArgs.Add(args[i]);

            return NewArgs.ToArray();
        }

        private static void ParamsValidation()
        {
            bool error = false;

            if (alternativs != null)
            {

                var RequiredAlternatives = alternativs.Where(a => a.Required == true).ToArray();
                if (RequiredAlternatives != null)
                {
                    for (int i = 0; i < RequiredAlternatives.Length; i++)
                    {
                        //опираюсь на обязательность ввода параметров
                        bool Isset = false;
                        for (int j = 0; j < RequiredAlternatives[i].Alternatives.Length; j++)
                        {
                            if (RequiredAlternatives[i].Alternatives[j].WasInitialized)
                            {
                                Isset = true;
                                break;
                            }
                        }

                        if (!Isset)
                        {
                            error = true;
                            Console.Error.WriteLine("Не введен один из обязательных альтернативных параметров: ");
                            for (int j = 0; j < RequiredAlternatives[i].Alternatives.Length; j++)
                            {
                                Console.Error.Write(RequiredAlternatives[i].Alternatives[j].Key);

                            }
                        }
                    }
                }

                var otherAlternatives = alternativs.Where(a => a.Required == false).ToArray();
                if (otherAlternatives != null)
                {
                    for (int i = 0; i < otherAlternatives.Length; i++)
                    {
                        int IssetCount = 0;
                        for (int j = 0; j < otherAlternatives[i].Alternatives.Length; j++)
                        {
                            if (otherAlternatives[i].Alternatives[j].WasInitialized)
                                IssetCount++;

                        }

                        if (IssetCount > 1)
                        {
                            error = true;
                            Console.Error.WriteLine("Ошибка при вводе необязательных альтернативных параметров(Указано несколько параметров вместо одного): ");
                            for (int j = 0; j < otherAlternatives[i].Alternatives.Length; j++)
                            {
                                Console.Error.Write(otherAlternatives[i].Alternatives[j].Key);
                            }
                        }
                    }
                }
            }
            //обработка обязательных параметров
            var requiredParams = AllParams.Where(p => p.Required == true).ToArray();
            if (requiredParams != null)
            {
                for (int i = 0; i < requiredParams.Length; i++)
                {
                    if (!requiredParams[i].ValidateForRequired())
                    {
                        Console.WriteLine("Отсутствует обязательный параметр {0}", requiredParams[i].Key);
                        error = true;
                    }
                }
            }

            if (error)
                Environment.Exit(1);
        }
    }
}
