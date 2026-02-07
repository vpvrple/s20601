using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;

namespace s20601.Services.External.Azure;

public class AzureBlobService : IAzureBlobService
{
    private readonly BlobServiceClient _blobServiceClient;

    public AzureBlobService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task<string> UploadImage(AzureBlobType containerType, Stream stream, string fileName)
    {
        var containerName = containerType.GetContainerName();
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        var blobClient = containerClient.GetBlobClient(fileName);

        await blobClient.UploadAsync(stream);

        return blobClient.Uri.ToString();
    }

    public async Task<Response> DeleteImage(AzureBlobType containerType, string fileName)
    {
        var containerName = containerType.GetContainerName();
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        var blobClient = containerClient.GetBlobClient(fileName);

        return await blobClient.DeleteAsync(DeleteSnapshotsOption.IncludeSnapshots);
    }

    public async Task<string?> DownloadImage(AzureBlobType containerType, string fileName)
    {
        var containerName = containerType.GetContainerName();
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(fileName);

        if (!await blobClient.ExistsAsync())
        {
            return null;
        }

        return blobClient.Uri.ToString();
    }
}
