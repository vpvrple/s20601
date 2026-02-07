using s20601.Data.Interfaces;

namespace s20601.Data.Models;

public class Movie : INavigable
{
    public int Id { get; set; }
    public string? IMDBId { get; init; }
    public string Title { get; set; } = null!;
    public string OriginalTitle { get; set; } = null!;
    public int StartYear { get; set; }
    public int? EndYear { get; set; }
    public int RuntimeMinutes { get; set; }
    public string TitleType { get; set; } = null!;
    public string? PosterPath { get; set; }
    public string? Overview { get; set; }
    public virtual ICollection<MovieCollectionMovie> MovieCollectionMovies { get; set; } = new List<MovieCollectionMovie>();
    public virtual ICollection<MovieCrew> MovieCrews { get; set; } = new List<MovieCrew>();
    public virtual ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    public virtual MovieOfTheDay? MovieOfTheDay { get; set; }
    public virtual ICollection<MovieRate> MovieRates { get; set; } = new List<MovieRate>();
    public virtual ICollection<MovieUpdateRequest> MovieUpdateRequests { get; set; } = new List<MovieUpdateRequest>();
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    public string GetUrl()
    {
        return $"movie/{Id}";
    }

    public override string ToString()
    {
        return Title;
    }
}
