using MediatR;
using s20601.Services.External.TMDB;

namespace s20601.Events.Queries;

public class GetTMDBMovieImageHandler(ITmdbLibClient tmdbLibClient) : IRequestHandler<GetTMDBMovieImageQuery, string?>
{
    public async Task<string?> Handle(GetTMDBMovieImageQuery request, CancellationToken cancellationToken)
    {
        var posterUrl = await tmdbLibClient.GetPosterUrlByImdbIdAsync(request.ImdbId);

        return posterUrl;
    }
}
