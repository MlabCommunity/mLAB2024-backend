using Microsoft.AspNetCore.Mvc.Testing;

namespace Api.IntegrationTests.ApiTests;

public class QuizzesApiTests
{
    protected readonly HttpClient _httpClient;

    public QuizzesApiTests()
    {
        var webApplicationFactory = new WebApplicationFactory<Program>();
        _httpClient = webApplicationFactory.CreateClient();
    }
}