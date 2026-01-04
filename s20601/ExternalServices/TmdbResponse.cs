using System.Text.Json.Serialization;

namespace s20601.ExternalServices;

public class TmdbResponse
{
    [JsonPropertyName("movie_results")]
    public List<MovieResult>? MovieResults { get; set; }
}

