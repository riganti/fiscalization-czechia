namespace Mews.Eet.Dto
{
    public sealed class PostResponse
    {
        public PostResponse(string responseBody, PostRequest request, long duration)
        {
            Body = responseBody;
            Request = request;
            Duration = duration;
        }

        public string Body { get; }

        public PostRequest Request { get; }

        public long Duration { get; }
    }
}
