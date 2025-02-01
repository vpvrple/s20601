using System;
using System.Collections.Generic;

namespace s20601.Data.Models;

public partial class MovieUpdateRequest
{
    public int Id { get; set; }

    public int IdUser { get; set; }

    public string Description { get; set; } = null!;

    public bool? IsAccepted { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Movie_Id { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
