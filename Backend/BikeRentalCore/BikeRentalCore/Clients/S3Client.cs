using Amazon;
using Amazon.Internal;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace BikeRentalCore.Clients;

public class S3Client : IStorageClient
{

    private readonly AmazonS3Client _client;

    public S3Client(IConfiguration configuration)
    {
        string awsKeyId = Environment.GetEnvironmentVariable("AWS_KEY_ID") 
            ?? configuration["AWS:KeyId"]!;
        string awsSecret = Environment.GetEnvironmentVariable("AWS_KEY_SECRET")
            ?? configuration["AWS:KeySecret"]!;

        BasicAWSCredentials credentials = new(awsKeyId, awsSecret);


        AmazonS3Config config = new()
        {
            RegionEndpoint = RegionEndpoint.USEast2
        };

        _client = new AmazonS3Client(credentials, config);


    }

    public async Task<string> UploadFileAsync(IFormFile file, string key)
    {
        using var inputStream = new MemoryStream();

        file.CopyTo(inputStream);

        TransferUtilityUploadRequest request = new()
        {
            BucketName = "bikerentals",
            Key = key,
            InputStream = inputStream,
            ContentType = file.ContentType,
            StorageClass = S3StorageClass.Standard,
        };

        TransferUtility transferUtility = new (_client);

        await transferUtility.UploadAsync(request);

        return "https://bikerentals.s3.us-east-2.amazonaws.com/" + key;
    }
}
