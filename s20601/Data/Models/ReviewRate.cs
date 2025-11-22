namespace s20601.Data.Models;

public partial class ReviewRate
{
    public int Id { get; set; }

    public string IdUser { get; set; } = null!;

    public int Review_Id { get; set; }

    public int Rating { get; set; }

    public DateTime RatedAt { get; set; }

    public virtual ApplicationUser IdUserNavigation { get; set; } = null!;

    public virtual Review Review { get; set; } = null!;
}
