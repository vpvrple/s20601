namespace s20601.Data.Models.DTOs;

public class UserRatingSummary
{
    public double AvgMovieRating { get; set; }
    public double MedMovieRating { get; set; }
    public int TotalRatings { get; set; }
    public int TotalReviews { get; set; }
    public List<RatingDistribution> RatingDistribution { get; set; }
}
