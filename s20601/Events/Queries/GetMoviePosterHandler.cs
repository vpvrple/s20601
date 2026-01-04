using MediatR;
using s20601.ExternalServices;

namespace s20601.Events.Queries;

public class GetMoviePosterHandler(ITmdbLibClient tmdbLibClient) : IRequestHandler<GetMoviePosterQuery, string?>
{
    public async Task<string?> Handle(GetMoviePosterQuery request, CancellationToken cancellationToken)
    {
        var posterUrl = await tmdbLibClient.GetPosterUrlByImdbIdAsync(request.ImdbId);

        return posterUrl;
    }
}
