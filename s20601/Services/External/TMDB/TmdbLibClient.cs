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

        return string.IsNullOrEmpty(movie?.PosterPath) ? null : $"{ImageBaseUrl}{movie.PosterPath}";
    }

    public async Task<string?> GetMovieOverviewByImdbIdAsync(string imdbId)
    {
        var response = await _httpClient.GetFromJsonAsync<TmdbResponse>(
            $"find/{imdbId}?api_key={_apiKey}&external_source=imdb_id");

        var movie = response?.MovieResults?.FirstOrDefault();

        return string.IsNullOrEmpty(movie?.Overview) ? null : movie.Overview;
    }
    
    public async Task<string?> GetPersonaImageUrlByImdbIdAsync(string imdbId)
    {
        var response = await _httpClient.GetFromJsonAsync<TmdbResponse>(
            $"find/{imdbId}?api_key={_apiKey}&external_source=imdb_id");

        var person = response?.PersonResults.FirstOrDefault();

        return string.IsNullOrEmpty(person.ProfilePath) ? null : $"{ImageBaseUrl}{person.ProfilePath}";
    }

    public async Task<string?> GetPersonaBiographyByImdbIdAsync(string imdbId)
    {
        var response = await _httpClient.GetFromJsonAsync<TmdbResponse>(
            $"find/{imdbId}?api_key={_apiKey}&external_source=imdb_id");

        var person = response?.PersonResults?.FirstOrDefault();

        if (person == null)
        {
            return null;
        }

        var personDetails = await _httpClient.GetFromJsonAsync<PersonResult>(
            $"person/{person.Id}?api_key={_apiKey}");

        return string.IsNullOrEmpty(personDetails?.Biography) ? null : personDetails.Biography;
    }
}