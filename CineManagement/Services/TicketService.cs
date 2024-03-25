using CineManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CineManagement.Services
{
    public class TicketService
    {
        private MovieService movieService = new MovieService();

        public bool AddTicket(Ticket ticket)
        {
            using (var _context = new CinemaManagementContext())
            {
                var existingTicket = _context.Tickets.FirstOrDefault(x => x.MovieId == ticket.MovieId && x.ProjectorId == ticket.ProjectorId && x.SeatId == ticket.SeatId);
                
                if (existingTicket == null)
                {
                    _context.Tickets.Add(ticket);

                    try
                    {
                        _context.SaveChanges();

                        return true;
                    }
                    catch (DbUpdateException ex)
                    {
                        throw new Exception("An error occurred while saving the ticket.", ex);
                    }

                }
                else
                {
                    throw new Exception("Ticket already exists.");
                }
            }
        }

        public List<Ticket> GetTicketsByMovieAndShowtime(int movieId, int projectorId)
        {
            using (var context = new CinemaManagementContext())
            {
                List<Ticket> tickets = context.Tickets.Where(ticket => ticket.MovieId == movieId && ticket.ProjectorId == projectorId).ToList();

                if (tickets == null)
                {
                    throw new Exception("Ticket not found");
                }
                else
                {
                    return tickets;
                }
            }
        }

        public List<Ticket> GetTicketsByUserId(int userId)
        {
            using (var context = new CinemaManagementContext())
            {
                List<Ticket> tickets = context.Tickets.Include(ticket => ticket.Movie)
                                                      .Include(ticket => ticket.Projector)
                                                      .Where(ticket => ticket.UserId == userId)
                                                      .ToList();

                if (tickets == null)
                {
                    throw new Exception("No movie found.");
                }
                else
                {
                    return tickets;
                }
            }
        }
    }
}
