namespace s20601.Services.External.TMDB;

public class TmdbLibClient : ITmdbLibClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    private const string ImageBaseUrl = "https://image.tmdb.org/t/p/w342";

    public TmdbLibClient(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _apiKey = config["Tmdb:ApiKey"];
    }

    public async Task<string?> GetPosterUrlByImdbIdAsync(string imdbId)
    {
        var response = await _httpClient.GetFromJsonAsync<TmdbResponse>(
            $"find/{imdbId}?api_key={_apiKey}&external_source=imdb_id");

        var movie = response?.MovieResults?.FirstOrDefault();

        if (string.IsNullOrEmpty(movie?.PosterPath))
        {
            return null;
        }

        return $"{ImageBaseUrl}{movie.PosterPath}";
    }

    public async Task<string?> GetMovieOverviewByImdbIdAsync(string imdbId)
    {
        var response = await _httpClient.GetFromJsonAsync<TmdbResponse>(
            $"find/{imdbId}?api_key={_apiKey}&external_source=imdb_id");

        var movie = response?.MovieResults?.FirstOrDefault();

        if (string.IsNullOrEmpty(movie?.Overview))
        {
            return null;
        }

        return movie.Overview;
    }
}