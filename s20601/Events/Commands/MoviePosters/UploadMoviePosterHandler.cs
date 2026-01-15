using MediatR;
using s20601.Events.Commands;
using s20601.Services.External.Azure;

public class UploadMoviePosterHandler : IRequestHandler<UploadMoviePosterCommand, string?>
{
    private readonly IAzureBlobService _azureBlobService;

    public UploadMoviePosterHandler(IAzureBlobService azureBlobService)
    {
        _azureBlobService = azureBlobService;
    }

    public async Task<string?> Handle(UploadMoviePosterCommand request, CancellationToken cancellationToken)
    {
        return await _azureBlobService.UploadImage(
            request.AzureBlobType,
            request.FileStream,
            request.FileName
        );
    }
}