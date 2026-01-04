namespace ETL.Models;

internal class IMDBTitleCrew
{
    public string tconst { get; set; }
    public List<string> directors { get; set; } = new();
    public List<string> writers { get; set; } = new();
}