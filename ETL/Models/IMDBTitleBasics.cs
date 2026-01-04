namespace ETL.Models;

public class IMDBTitleBasics
{
    public string tconst { get; set; }
    public string titleType { get; set; }
    public string primaryTitle { get; set; }
    public string originalTitle { get; set; }
    public bool isAdult { get; set; }
    public string startYear { get; set; }
    public string endYear { get; set; }
    public string runtimeMinutes { get; set; }
    public string genres { get; set; }
}