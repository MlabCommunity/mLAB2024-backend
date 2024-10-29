namespace QuizBackend.Infrastructure.Services.Delegating;

public class GroqDelegatingHandler : DelegatingHandler
{
    private readonly string _endpoint;
    public GroqDelegatingHandler(string endpoint) : base(new HttpClientHandler())
    {
        _endpoint = endpoint;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.RequestUri = new Uri(request.RequestUri.ToString().Replace("https://api.openai.com/v1", _endpoint));

        return await base.SendAsync(request, cancellationToken);
    }
}