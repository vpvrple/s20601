using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class Movie
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string OriginalTitle { get; set; } = null!;

    public int StartYear { get; set; }

    public int? EndYear { get; set; }

    public int RuntimeMinutes { get; set; }

    public string TitleType { get; set; } = null!;

    public virtual ICollection<MovieCollectionMovie> MovieCollectionMovies { get; set; } = new List<MovieCollectionMovie>();

    public virtual ICollection<MovieCrew> MovieCrews { get; set; } = new List<MovieCrew>();

    public virtual ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();

    public virtual ICollection<MovieRate> MovieRates { get; set; } = new List<MovieRate>();

    public virtual ICollection<MovieUpdateRequest> MovieUpdateRequests { get; set; } = new List<MovieUpdateRequest>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
