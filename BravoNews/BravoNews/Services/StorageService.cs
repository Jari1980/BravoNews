using Azure.Storage.Blobs;

namespace BravoNews.Services
{
    public class StorageService : IStorageService
    {
        private readonly IConfiguration _configuration;
        private readonly BlobServiceClient _blobServiceClient;
        public StorageService(IConfiguration configuration)
        {
            _configuration = configuration;
            _blobServiceClient = new BlobServiceClient(_configuration["AzureWebJobsStorage"]);
        }
        public Uri UploadBlob (string pathfile)
        {
            string containerName = "news-images";
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/articles");
            BlobClient blobClient = containerClient.GetBlobClient(pathfile);
            string fileNameWithPath = Path.Combine(path, pathfile);
            blobClient.Upload(fileNameWithPath, true); //fileNameWithPath? path

            return blobClient.Uri;
        }
        public Uri GetBlob(string blobName, string containerName, string cat)
        {
            //string containerName = "news-images-xs";
            BlobContainerClient containerClient = _blobServiceClient
                .GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(cat + "/" + blobName);
            
            return blobClient.Uri;
        }
    }
}
