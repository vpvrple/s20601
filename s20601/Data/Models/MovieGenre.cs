namespace s20601.Data.Models;

public partial class MovieGenre
{
    public int Id { get; set; }

    public int Movie_Id { get; set; }

    public int Genre_Id { get; set; }

    public virtual Genre Genre { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
