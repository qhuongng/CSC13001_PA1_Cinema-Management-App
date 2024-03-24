    using System;
using System.Collections.Generic;

namespace CineManagement.Models;

public partial class MovieInfo
{
    public int MovieId { get; set; }

    public bool IsSelling { get; set; }

    public int DailyShowtime { get; set; }

    public int WeeklyShowtime { get; set; }

    public int MonthlyShowtime { get; set; }

    public int SoldTicket { get; set; }

    public long TicketRevenue { get; set; }

    public virtual Movie Movie { get; set; } = null!;
}
