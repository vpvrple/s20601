using Azure;
using MediatR;
using s20601.Services.External.Azure;

namespace s20601.Events.Commands.UserAvatars;

public class UserAvatarUpdatedHandler : IRequestHandler<UserAvatarUpdatedCommand, Response?>
{
    private readonly IAzureBlobService _azureBlobService;

    public UserAvatarUpdatedHandler(IAzureBlobService azureBlobService)
    {
        _azureBlobService = azureBlobService;
    }

    public async Task<Response> Handle(UserAvatarUpdatedCommand request, CancellationToken cancellationToken)
    {
        return await _azureBlobService.DeleteImage(
            request.AzureBlobType,
            request.FileName
        );
    }
}