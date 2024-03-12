using System;
using System.Collections.Generic;

namespace CineManagement.Models;

public partial class Director
{
    public int DirectorId { get; set; }

    public string DirectorName { get; set; } = null!;

    public byte[]? Avatar { get; set; }

    public string? ShotDes { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
