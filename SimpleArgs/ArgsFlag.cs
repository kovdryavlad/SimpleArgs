using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleArgs
{
    public class ArgFlag
    {
        public static implicit operator bool(ArgFlag a)
        {
            return a.WasInitialized;
        }

        public string Description       { get; private set; }
        public string Key               { get; private set; }
        public string LongKey           { get; private set; }
        public bool   WasInitialized    { get; private set; } 

        public ArgFlag(string key)
            : this(key, "", "")
        {
            
        }

        public ArgFlag(string key, string description)
            :this(key, description, "")
        {

        }

        public ArgFlag(string key, string longkey, string description)
        {
            this.Key = key;
            this.LongKey = longkey;
            this.Description = description;
        }
    }
}
