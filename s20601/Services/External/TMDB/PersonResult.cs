using System.Text.Json.Serialization;

namespace s20601.Services.External.TMDB;

public class PersonResult
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("profile_path")]
    public string? ProfilePath { get; set; }

    [JsonPropertyName("biography")]
    public string? Biography { get; set; }
}
