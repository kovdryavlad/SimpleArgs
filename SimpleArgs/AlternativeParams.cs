namespace SimpleArgs
{
    class AlternativeParams
    {
        public ArgFlag[] Alternatives { get; private set; }

        public AlternativeParams(params ArgFlag[] args)
        {
            Alternatives = args;
        }
    }
}
