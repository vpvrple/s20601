using Azure;
using MediatR;
using s20601.Services.External.Azure;

namespace s20601.Events.Commands;

public record MoviePosterUpdatedCommand(
    AzureBlobType AzureBlobType,
    string FileName
) : INotification, IRequest<Response>;