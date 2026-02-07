using MediatR;
using s20601.Services.External.Azure;

namespace s20601.Events.Commands.UserAvatars;

public record UploadUserAvatarCommand(
    AzureBlobType AzureBlobType,
    Stream FileStream,
    string FileName
) : IRequest<string?>;