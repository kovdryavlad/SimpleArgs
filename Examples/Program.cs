using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleArgs;
using System.Diagnostics;

namespace Examples
{
    class Program
    {
        static ArgFlag Verbose = new ArgFlag("v", "verboze", "Вербоза");
        static ArgFlag Hight = new ArgFlag("h", "hight", "Высота");

        static ArgParam<int> zu = new ArgParam<int>("zu", "зюбаба", "Дебют Зю") { Required = true };
        static ArgParam<int> intParam = new ArgParam<int>("n", "number", "Описание интового параметра") { Required = true };
        static ArgService service = new ArgService("Справка по использованию программы", new AlternativeParams(true, zu, intParam));

        static void Main(string[] args)
        {
            ParamsHandler.Handle(args, typeof(Program));
            Console.ReadLine();
        }
    }
}
