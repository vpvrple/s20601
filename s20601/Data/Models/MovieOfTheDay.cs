namespace s20601.Data.Models;

public partial class MovieOfTheDay
{
    public int Id { get; set; }

    public int Movie_Id { get; set; }

    public DateTime Date { get; set; }

    public virtual Movie Movie { get; set; } = null!;
}
