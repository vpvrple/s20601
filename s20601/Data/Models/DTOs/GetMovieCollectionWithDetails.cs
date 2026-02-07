namespace s20601.Data.Models.DTOs;

public class GetMovieCollectionWithDetails
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public CollectionType Type { get; set; }

    public CollectionVisibility Visibility { get; set; }

    public IDictionary<ApplicationUser, CollectionRole> Members { get; set; }

    public int MovieCount { get; set; }
}