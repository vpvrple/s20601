namespace s20601.Services.External.TMDB;

public interface ITmdbLibClient
{
    Task<string?> GetPosterUrlByImdbIdAsync(string imdbId);
    Task<string?> GetMovieOverviewByImdbIdAsync(string imdbId);
    Task<string?> GetPersonaImageUrlByImdbIdAsync(string imdbId);
    Task<string?> GetPersonaBiographyByImdbIdAsync(string imdbId);
}
