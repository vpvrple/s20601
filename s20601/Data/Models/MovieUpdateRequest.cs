namespace s20601.Data.Models;

public class MovieUpdateRequest
{
    public int Id { get; set; }
    public string IdUser { get; set; } = null!;
    public string? Description { get; set; }
    public MovieUpdateRequestStatus? Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? Movie_Id { get; set; }
    public string? NewTitle { get; set; }
    public string? NewOriginalTitle { get; set; }
    public int? NewStartYear { get; set; }
    public int? NewEndYear { get; set; }
    public int? NewRuntimeMinutes { get; set; }
    public string? NewTitleType { get; set; }
    public string? NewOverview { get; set; }
    public string? NewPosterPath { get; set; }
    public virtual ApplicationUser IdUserNavigation { get; set; } = null!;
    public virtual Movie? Movie { get; set; }
    public virtual ICollection<Genre> NewGenres { get; set; } = new List<Genre>();
    public virtual ICollection<MovieUpdateRequestCrew> NewCrew { get; set; } = new List<MovieUpdateRequestCrew>();
}

public enum MovieUpdateRequestStatus
{
    Open,
    Approved,
    Rejected,
}