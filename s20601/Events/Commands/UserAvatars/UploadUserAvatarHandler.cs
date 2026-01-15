using MediatR;
using s20601.Events.Commands;
using s20601.Services.External.Azure;

public class UploadUserAvatarHandler : IRequestHandler<UploadUserAvatarCommand, string?>
{
    private readonly IAzureBlobService _azureBlobService;

    public UploadUserAvatarHandler(IAzureBlobService azureBlobService)
    {
        _azureBlobService = azureBlobService;
    }

    public async Task<string?> Handle(UploadUserAvatarCommand request, CancellationToken cancellationToken)
    {
        return await _azureBlobService.UploadImage(
            request.AzureBlobType,
            request.FileStream,
            request.FileName
        );
    }
}