using MediatR;
using s20601.Services.External.Azure;

namespace s20601.Events.Queries;

public class GetAzureMovieImageHandler(IAzureBlobService azureBlobService) : IRequestHandler<GetAzureMovieImageQuery, string?>
{
    public async Task<string?> Handle(GetAzureMovieImageQuery request, CancellationToken cancellationToken)
    {
        var posterUrl = await azureBlobService.DownloadImage(request.AzureBlobType, request.BlobName);

        return posterUrl;
    }
}
