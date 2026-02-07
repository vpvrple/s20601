using Azure;
using MediatR;
using s20601.Events.Commands.MoviePosters;
using s20601.Services.External.Azure;

public class MoviePosterUpdatedHandler : IRequestHandler<MoviePosterUpdatedCommand, Response>
{
    private readonly IAzureBlobService _azureBlobService;

    public MoviePosterUpdatedHandler(IAzureBlobService azureBlobService)
    {
        _azureBlobService = azureBlobService;
    }

    public async Task<Response> Handle(MoviePosterUpdatedCommand request, CancellationToken cancellationToken)
    {
        return await _azureBlobService.DeleteImage(
            request.AzureBlobType,
            request.FileName
        );
    }
}