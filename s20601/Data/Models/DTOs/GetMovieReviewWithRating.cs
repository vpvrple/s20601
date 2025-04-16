namespace s20601.Data.Models.DTOs;

public class GetMovieReviewWithRating
{
    public int Id { get; set; }
    public string AuthorId { get; set; } = null!;
    public string Nickname { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string Content { get; set; } = null!;
    public int LikeRating { get; set; }
    public int DislikeRating { get; set; }
}
