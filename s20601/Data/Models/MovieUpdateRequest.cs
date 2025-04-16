namespace s20601.Data.Models;

public partial class MovieUpdateRequest
{
    public int Id { get; set; }

    public string IdUser { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool? IsAccepted { get; set; }

    public DateTime CreatedAt { get; set; }

    public int Movie_Id { get; set; }

    public virtual ApplicationUser IdUserNavigation { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
