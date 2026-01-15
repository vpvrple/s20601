using Azure;

namespace s20601.Services.External.Azure;

public interface IAzureBlobService
{
    Task<string> UploadImage(AzureBlobType containerType, Stream stream, string fileName);
    Task<string?> DownloadImage(AzureBlobType containerType, string fileName);
    Task<Response> DeleteImage(AzureBlobType containerType, string fileName);
}
