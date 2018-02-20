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
        public static void Handle(string[] args)
        {
            var StartClassName = GetStartClassName();
          
        }

        private static object GetStartClassName()
        {
            StackTrace tr = new StackTrace();
            var FrameWithMain = tr.GetFrame(tr.FrameCount-1);

            var fields = FrameWithMain.GetMethod().ReflectedType.GetFields(BindingFlags.Static);

            

            var fg = FrameWithMain.GetMethod();
            var c = fg.ReflectedType;
            var zu = c.GetFields();

            throw new NotImplementedException();
        }
    }
}
