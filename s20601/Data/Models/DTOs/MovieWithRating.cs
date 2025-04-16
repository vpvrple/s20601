namespace s20601.Data.Models.DTOs;

public class MovieWithRating
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int StartYear { get; set; }
    public int Runtime { get; set; }
    public MovieRatingSummary MovieRatingSummary { get; set; }
}
