using BikeRentalNotificationService;
using MassTransit;
using System.Net.Http.Json;

namespace Messaging;

public class AuthorizedRentalConsumer(ILogger<AuthorizedRentalConsumer> logger, IEmailClient emailClient, IConfiguration configuration)
    : IConsumer<AuthorizedRentalEvent>
{

    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri(Environment.GetEnvironmentVariable("IDENTITY_SERVICE_URL")
            ?? configuration["IdentityService:BaseUrl"]!)
    };

    public async Task Consume(ConsumeContext<AuthorizedRentalEvent> context)
    {
        logger.LogInformation("Reading data from Queue");

        string rentalCode = context.Message.RentalCode;
        string userId = context.Message.UserId;

        string url = $"/identity/user/{userId}";

        HttpRequestMessage request = new(HttpMethod.Get, url);

        string apiKey = Environment.GetEnvironmentVariable("API_KEY")
            ?? configuration["Api:Key"]!;

        request.Headers.Add("Api-Key", apiKey);

        HttpResponseMessage response = await _httpClient.SendAsync(request);

        if(response.IsSuccessStatusCode)
        {
            var user = await response.Content.ReadFromJsonAsync<User>();

            if (user is null)
            {
                logger.LogError($"User {userId} not found");
                return;
            }

            string content = CreateHtmlContent(user.FullName, rentalCode);

            await emailClient.SendAsync("noreply@thiagoabdul.com.br", user.Email, "BikeRentals", content);
        }
        else
        {
            string message = await response.Content.ReadAsStringAsync();
            logger.LogError(message);
        }
    }

    private static string CreateHtmlContent(string fullName, string rentalCode)
    {
        string baseHtml = Templates.HtmlNotification;
        return baseHtml
            .Replace("[fullName]", fullName)
            .Replace("[rentalCode]", rentalCode);
    }
}

class User
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool AccountEnabled { get; set; }
    public DateTime CreatedAt { get; set; }
}