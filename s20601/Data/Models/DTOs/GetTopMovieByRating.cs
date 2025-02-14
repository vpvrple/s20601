namespace s20601.Data.Models.DTOs
{
    public class GetTopMovieByRating
    {
        public string Id { get; set; }
        public string Title { get; set; } = null!;
        public double Rating { get; set; }
        public int RateCount { get; set; }
        public int Runtime { get; set; }
        public int StartYear { get; set; }
    }
}
