using MediatR;
using s20601.ExternalServices;

namespace s20601.Events.Queries;

public class GetMovieOverviewHandler(ITmdbLibClient tmdbLibClient) : IRequestHandler<GetMovieOverviewQuery, string?>
{
    public async Task<string?> Handle(GetMovieOverviewQuery request, CancellationToken cancellationToken)
    {
        var overview = await tmdbLibClient.GetMovieOverviewByImdbIdAsync(request.ImdbId);

        return overview;
    }
}
