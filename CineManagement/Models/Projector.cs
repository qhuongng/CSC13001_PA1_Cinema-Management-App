using System;
using System.Collections.Generic;

namespace CineManagement.Models;

public partial class Projector
{
    public int ProjectorId { get; set; }

    public DateTime ProjectorInfo { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
