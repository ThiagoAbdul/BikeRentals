namespace BikeRentalCore.Clients;

public interface IStorageClient
{
    Task<string> UploadFileAsync(IFormFile file, string key);
}
