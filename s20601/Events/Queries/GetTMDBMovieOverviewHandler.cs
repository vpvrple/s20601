using MediatR;
using s20601.Services.External.TMDB;

namespace s20601.Events.Queries;

public class GetTMDBMovieOverviewHandler(ITmdbLibClient tmdbLibClient) : IRequestHandler<GetTMDBMovieOverviewQuery, string?>
{
    public async Task<string?> Handle(GetTMDBMovieOverviewQuery request, CancellationToken cancellationToken)
    {
        var overview = await tmdbLibClient.GetMovieOverviewByImdbIdAsync(request.ImdbId);

        return overview;
    }
}
