namespace SimpleArgs
{
    public class AlternativeParams
    {
        public ArgFlag[] Alternatives { get; private set; }

        public bool Required { get; internal set; }

        public AlternativeParams(bool required, params ArgFlag[] args)
        {
            Required = required;
            Alternatives = args;
        }
    }
}
