using System.Text.Json.Serialization;

namespace s20601.Services.External.TMDB;

public class PersonResult
{
    [JsonPropertyName("profile_path")]
    public string? ProfilePath { get; set; }
}
