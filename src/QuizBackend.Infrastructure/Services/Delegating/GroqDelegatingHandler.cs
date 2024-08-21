
namespace QuizBackend.Infrastructure.Services.Delegating
{
    public class GroqDelegatingHandler : DelegatingHandler
    {
        public GroqDelegatingHandler() : base(new HttpClientHandler()) { }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.RequestUri = new Uri(request.RequestUri.ToString().Replace("https://api.openai.com/v1", "https://api.groq.com/openai/v1"));

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
