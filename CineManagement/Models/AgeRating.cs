using System;
using System.Collections.Generic;

namespace CineManagement.Models;

public partial class AgeRating
{
    public string AgeId { get; set; } = null!;

    public string AgeDef { get; set; } = null!;

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
