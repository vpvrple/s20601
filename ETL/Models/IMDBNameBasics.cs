namespace ETL.Models;

public class IMDBNameBasics
{
    public string nconst { get; set; }
    public string primaryName { get; set; }
    public string birthYear { get; set; }
    public string deathYear { get; set; }
    public List<string> primaryProfessions { get; set; } = new();
    public string knownForTitles { get; set; }
}