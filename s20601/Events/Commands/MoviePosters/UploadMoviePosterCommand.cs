using MediatR;
using s20601.Services.External.Azure;

namespace s20601.Events.Commands;

public record UploadMoviePosterCommand(
    AzureBlobType AzureBlobType,
    Stream FileStream,
    string FileName
) : IRequest<string?>;