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
        static void Main(string[] args)
        {
            Trace.WriteLine("123");
            Debug.WriteLine("hello");
            //ArgsBase<int> a = new ArgsBase<int>("file", "Key to Set file", false);
        }
    }
}
