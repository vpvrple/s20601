﻿using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class Movie
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string OriginalTitle { get; set; } = null!;

    public int StartYear { get; set; }

    public int? EndYear { get; set; }

    public int RuntimeMinutes { get; set; }

    public string TitleType { get; set; } = null!;

    public virtual ICollection<MovieCollectionMovie> MovieCollectionMovies { get; set; } = [];

    public virtual ICollection<MovieCrew> MovieCrews { get; set; } = [];

    public virtual ICollection<MovieGenre> MovieGenres { get; set; } = [];

    public virtual MovieOfTheDay? MovieOfTheDay { get; set; }

    public virtual ICollection<MovieRate> MovieRates { get; set; } = [];

    public virtual ICollection<MovieUpdateRequest> MovieUpdateRequests { get; set; } = [];

    public virtual ICollection<Review> Reviews { get; set; } = [];

    public override string ToString()
    {
        return Title;
    }
}
