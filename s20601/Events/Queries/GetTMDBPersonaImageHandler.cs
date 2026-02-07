using MediatR;
using s20601.Services.External.TMDB;

namespace s20601.Events.Queries;

public class GetTMDBPersonaImageHandler(ITmdbLibClient tmdbLibClient) : IRequestHandler<GetTMDBPersonaImageQuery, string?>
{
    public async Task<string?> Handle(GetTMDBPersonaImageQuery request, CancellationToken cancellationToken)
    {
        var imageUrl = await tmdbLibClient.GetPersonaImageUrlByImdbIdAsync(request.ImdbId);

        return imageUrl;
    }
}
