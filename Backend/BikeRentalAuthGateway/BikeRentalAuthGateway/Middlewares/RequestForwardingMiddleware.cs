using BikeRentalAuthGateway.Extensions;
using System.Runtime.InteropServices;

namespace BikeRentalAuthGateway.Middlewares;

public class RequestForwardingMiddleware(IHttpClientFactory httpClientFactory, IConfiguration configuration) : IMiddleware
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {

        if(context.Request.Path.StartsWithSegments("/auth") || context.Request.Path.StartsWithSegments("/identity"))
        {
            await next(context);
            return;

        }

        if (!context.User.Identity?.IsAuthenticated ?? false)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.CompleteAsync();
            return;
        }

        // Gateway
        await ForwardRequest(context);
        
    }

    private async Task ForwardRequest(HttpContext context)
    {
        string? targetServiceUrl = BuildForwardingUrl(context.Request);

        if(targetServiceUrl is null)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.CompleteAsync();
            return;
        }

        var forwardRequest = new HttpRequestMessage
        {
            Method = new HttpMethod(context.Request.Method),
            RequestUri = new Uri(targetServiceUrl),
            Content = new StreamContent(context.Request.Body)
        };


        foreach (var header in context.Request.Headers)
        {
            forwardRequest.Headers.TryAddWithoutValidation(header.Key, [.. header.Value]);
        }

        forwardRequest.Content.Headers.Add("Content-Type", "application/json");


        var response = await _httpClient.SendAsync(forwardRequest);


        context.Response.StatusCode = (int)response.StatusCode;
        context.Response.ContentType = "application/json";
        var responseContent = await response.Content.ReadAsStringAsync();


        await context.Response.WriteAsync(responseContent);
    }

    private string? BuildForwardingUrl(HttpRequest request)
    {
        string? path = request.Path.Value;

        if(string.IsNullOrEmpty(path))
            return null;

        if (path.Contains("/api"))
        {
            string baseUrl = configuration.Gateway().MainApiUrl;
            return baseUrl + path;
        }

        else return null;
    }
}
