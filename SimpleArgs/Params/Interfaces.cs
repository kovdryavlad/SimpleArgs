using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleArgs
{
    public interface IArgType
    {
        Type GetType { get; }
    }

    public interface IArgParamBase
    {
        bool Required   { get; set; }
        string Parametr { get; set; }
        string Key { get; }
        bool ValidateForRequired();
    }
}
