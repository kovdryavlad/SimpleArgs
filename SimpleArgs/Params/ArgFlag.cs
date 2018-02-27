namespace SimpleArgs
{
    public class ArgFlag:IInitializable
    {
        public static implicit operator bool(ArgFlag a)
        {
            return a.WasInitialized;
        }

        //конструктор для наследования
        internal ArgFlag(){}

        public string Description       { get; private set; }
        public string Key               { get; private set; }
        public string LongKey           { get; private set; }
        public bool   WasInitialized    { get; set; } 

        public ArgFlag(string key)
            : this(key, null, null)
        {
            
        }

        public ArgFlag(string key, string description)
            :this(key, null, description)
        {

        }

        public ArgFlag(string key, string longkey, string description)
        {
            Key = key;
            LongKey = longkey;
            Description = description;
        }
    }
}
