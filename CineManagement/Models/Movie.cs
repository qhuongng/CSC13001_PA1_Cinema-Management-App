using System;
using System.Collections.Generic;

namespace CineManagement.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string MovieName { get; set; } = null!;

    public int Duration { get; set; }

    public int ReleaseYear { get; set; }

    public double ImdbRating { get; set; }

    public int DirectorId { get; set; }

    public byte[]? Poster { get; set; }

    public string Certification { get; set; } = null!;

    public virtual AgeRating CertificationNavigation { get; set; } = null!;

    public virtual Director Director { get; set; } = null!;

    public virtual MovieInfo? MovieInfo { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual ICollection<Actor> Actors { get; set; } = new List<Actor>();

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
