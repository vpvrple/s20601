using Azure;
using MediatR;
using s20601.Services.External.Azure;

namespace s20601.Events.Commands.UserAvatars;

public record UserAvatarUpdatedCommand(
    AzureBlobType AzureBlobType,
    string FileName
) : IRequest<Response>;