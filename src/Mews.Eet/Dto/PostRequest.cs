namespace Mews.Eet.Dto
{
    public sealed class PostRequest
    {
        public PostRequest(string body, string operation)
        {
            Body = body;
            Operation = operation;
        }

        public string Body { get; }

        public string Operation { get; }
    }
}
