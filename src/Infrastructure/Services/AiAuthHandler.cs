using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class AiAuthHandler : DelegatingHandler
{
    private readonly IConfiguration _configuration;

    public AiAuthHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var apiKey = _configuration["OpenAi:ApiKey"]!;
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        return base.SendAsync(request, cancellationToken);
    }
    
}