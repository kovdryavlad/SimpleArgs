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
        static ArgFlag help = new ArgFlag("?", "help", "Справка");

        static void Main(string[] args)
        {
            ParamsHandler.Handle(args);

        }
    }
}
