namespace s20601.Data.Models;

public partial class MovieCollectionUser
{
    public int Id { get; set; }

    public string IdUser { get; set; } = null!;

    public int IdMovieCollection { get; set; }

    public virtual MovieCollection IdMovieCollectionNavigation { get; set; } = null!;

    public virtual ApplicationUser IdUserNavigation { get; set; } = null!;
    
    public CollectionRole Role { get; set; }
}

public enum CollectionRole
{
    Viewer = 0,      // Read-only (for private shared)
    Contributor = 1, // Can add/edit movies
    Owner = 2        // Can delete collection, manage users
}