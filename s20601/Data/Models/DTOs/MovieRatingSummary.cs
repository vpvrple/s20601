using s20601.Services.External.Azure;

namespace s20601.Data.Models.DTOs;

public class MovieRatingSummary
{
    public double AvgRating { get; set; }
    public int RateCount { get; set; }
    public SentimentType Sentiment { get; set; }
}
