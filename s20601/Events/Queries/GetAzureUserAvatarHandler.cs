using MediatR;
using s20601.Services.External.Azure;

namespace s20601.Events.Queries;

public class GetAzureUserAvatarHandler(IAzureBlobService azureBlobService) : IRequestHandler<GetAzureUserAvatarQuery, string?>
{
    public async Task<string?> Handle(GetAzureUserAvatarQuery request, CancellationToken cancellationToken)
    {
        var avatarUrl = await azureBlobService.DownloadImage(request.AzureBlobType, request.BlobName);

        return avatarUrl;
    }
}
