namespace s20601.ExternalServices;

public interface ITmdbLibClient
{
    Task<string?> GetPosterUrlByImdbIdAsync(string imdbId);
    Task<string?> GetMovieOverviewByImdbIdAsync(string imdbId);
}
