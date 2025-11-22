using s20601.Data.Interfaces;

namespace s20601.Data.Models;

public partial class MovieCollection : INavigable
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual MovieCollectionMovie? MovieCollectionMovie { get; set; }

    public virtual ICollection<MovieCollectionUser> MovieCollectionUsers { get; set; } = new List<MovieCollectionUser>();

    public string GetUrl()
    {
        return $"movie-collection/{Id}";
    }
    public override string ToString()
    {
        return Name;
    }
}
