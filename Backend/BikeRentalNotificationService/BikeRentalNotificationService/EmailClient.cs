using Amazon.SimpleEmailV2;
using Amazon.SimpleEmailV2.Model;

namespace BikeRentalNotificationService;

public class EmailClient : IEmailClient
{
    private readonly IConfiguration _configuration;

    private readonly AmazonSimpleEmailServiceV2Client _awsSesClient;

    public EmailClient(IConfiguration configuration)
    {
        _configuration = configuration;

        string accessKeyId = Environment.GetEnvironmentVariable("ACCESS_KEY_ID") 
            ?? _configuration["AWS:AccessKeyId"]!;

        string accessKeySecret = Environment.GetEnvironmentVariable("ACCESS_KEY_SECRET")
            ?? _configuration["AWS:AccessKeySecret"]!;



        _awsSesClient = new AmazonSimpleEmailServiceV2Client(accessKeyId, accessKeySecret, Amazon.RegionEndpoint.USEast2);


    }
    public async Task SendAsync(string sendFrom, string sendTo, string subject, string content)
    {
        SendEmailRequest request = new()
        {
            FromEmailAddress = sendFrom,
            Destination = new Destination
            {
                ToAddresses = [sendTo]
            },
            Content = new EmailContent
            {
                Simple = new Message
                {
                    Subject = new Content { Data = subject },
                    Body = new Body
                    {
                        Html = new Content { Data = content },
                    }
                }
            }
        };

        await _awsSesClient.SendEmailAsync(request);

    }
}
