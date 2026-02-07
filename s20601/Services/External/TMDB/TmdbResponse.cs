using System.Text.Json.Serialization;

namespace s20601.Services.External.TMDB;

public class TmdbResponse
{
    [JsonPropertyName("movie_results")]
    public List<MovieResult>? MovieResults { get; set; }

    [JsonPropertyName("person_results")]
    public List<PersonResult>? PersonResults { get; set; }
}
