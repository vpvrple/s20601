using MediatR;
using s20601.Services.External.TMDB;

namespace s20601.Events.Queries;

public class GetTMDBPersonaBiographyHandler(ITmdbLibClient tmdbLibClient) : IRequestHandler<GetTMDBPersonaBiographyQuery, string?>
{
    public async Task<string?> Handle(GetTMDBPersonaBiographyQuery request, CancellationToken cancellationToken)
    {
        var biography = await tmdbLibClient.GetPersonaBiographyByImdbIdAsync(request.ImdbId);

        return biography;
    }
}
