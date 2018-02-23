using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleArgs
{
    class TypesInfo
    {
        static ArgType<string> t1 = new ArgType<string>(s => s);
        static ArgType<int>    t2 = new ArgType<int>(s=>Convert.ToInt32(s));
        static ArgType<double> t3 = new ArgType<double>(s => Convert.ToDouble(s.Replace('.', ',')));
        
        public static List<IArgType> TypeList = new List<IArgType>(new IArgType[] { t1, t2, t3});
    }

    class ArgType<T>:IArgType
    {
        public Type GetType { get { return typeof(T); } }
        
        internal Func<string, T> Converter;

        public ArgType(Func<string,T> converter)
        {
            Converter = converter;
        }
    }

    public interface IArgType
    {
        Type GetType { get; }
    }
}
