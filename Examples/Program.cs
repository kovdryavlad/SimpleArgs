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
        static ArgFlag Verbose = new ArgFlag("v", "help", "Вербоза");

        static ArgParam<int> intParam = new ArgParam<int>("n","number", "Описание интового параметра");
        static ArgService service = new ArgService("Справка по использованию программы", null);

        static void Main(string[] args)
        {
            ParamsHandler.Handle(args, typeof(Program));
            Console.ReadLine();
        }
    }
}
