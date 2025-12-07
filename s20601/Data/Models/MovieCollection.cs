using s20601.Data.Interfaces;

namespace s20601.Data.Models;

public partial class MovieCollection : INavigable
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public CollectionType Type { get; set; }

    public CollectionVisibility Visibility { get; set; }
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

public enum CollectionType
{
    Custom = 0,      // Created by user (Deletable)
    MyRatings = 1,   // The rating target (Protected)
    WatchLater = 2,  // Future proofing (Protected)
}

public enum CollectionVisibility
{
    Private = 0,
    Public = 1
}