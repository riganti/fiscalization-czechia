namespace Mews.Eet.Dto
{
    public sealed class SoapInput<TIn>
        where TIn : class, new()
    {
        public SoapInput(TIn message, string operation)
        {
            Message = message;
            Operation = operation;
        }

        public TIn Message { get; }

        public string Operation { get; }
    }
}
