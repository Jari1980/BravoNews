namespace TimerNewsApp.Services
{
    public interface IStorageService
    {
        Uri UploadBlob(string pathfile);
        Uri GetBlob(string blobName, string containerName, string cat);
    }
}