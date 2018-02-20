namespace SimpleArgs
{
    class ArgService
    {
        public string HelpMessage { get; private set; }
        public AlternativeParams[] AlternativesParamsArray { get; private set; }

        public ArgService(string HelpMessage, params AlternativeParams[] AlternativeParamsList)
        {
            this.HelpMessage = HelpMessage;
            AlternativesParamsArray = AlternativeParamsList;
        }
    }
}
