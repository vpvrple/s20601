namespace s20601.Data.Models.DTOs
{
    public class GetMovieReviewWithRating
    {
        public string UserName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; } = null!;
        public int LikeRating { get; set; }
        public int DislikeRating { get; set; }
    }
}
