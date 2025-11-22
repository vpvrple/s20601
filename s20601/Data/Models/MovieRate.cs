namespace s20601.Data.Models;

public partial class MovieRate
{
    public string IdUser { get; set; } = null!;

    public int Rating { get; set; }

    public DateTime RatedAt { get; set; }

    public int Movie_Id { get; set; }

    public int Id { get; set; }

    public virtual ApplicationUser IdUserNavigation { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
