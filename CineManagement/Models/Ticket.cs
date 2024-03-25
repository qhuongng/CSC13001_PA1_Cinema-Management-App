using System;
using System.Collections.Generic;

namespace CineManagement.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int UserId { get; set; }

    public int MovieId { get; set; }

    public string SeatId { get; set; } = null!;

    public double Price { get; set; }

    public int ProjectorId { get; set; }

    public virtual Movie Movie { get; set; } = null!;

    public virtual Projector Projector { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
