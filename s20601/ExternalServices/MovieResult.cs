using System.Text.Json.Serialization;

namespace s20601.ExternalServices;

public class MovieResult
{
    [JsonPropertyName("poster_path")]
    public string? PosterPath { get; set; }

    [JsonPropertyName("overview")]
    public string? Overview { get; set; }
}
