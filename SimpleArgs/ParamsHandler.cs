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

        public static void GetStartClassName()
        {
            StackTrace tr = new StackTrace();
            var FrameWithMain = tr.GetFrame(tr.FrameCount-1);

            var fields = FrameWithMain.GetMethod().ReflectedType.GetFields(BindingFlags.Static | BindingFlags.NonPublic|BindingFlags.Public);


            Array.ForEach(fields, Console.WriteLine);
        }
    }
}
