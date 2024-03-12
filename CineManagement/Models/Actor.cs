using System;
using System.Collections.Generic;

namespace CineManagement.Models;

public partial class Actor
{
    public int ActorId { get; set; }

    public string ActorName { get; set; } = null!;

    public byte[]? Avatar { get; set; }

    public string? ShotDes { get; set; }

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
