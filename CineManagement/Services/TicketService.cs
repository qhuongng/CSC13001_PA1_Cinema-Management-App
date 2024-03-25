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

        public Ticket GetTicketInfo(int ticketId)
        {
            using(var context = new CinemaManagementContext())
            {
                Ticket ticket = context.Tickets.FirstOrDefault(x =>  x.TicketId == ticketId);
                
                if(ticket != null) 
                {
                    ticket.Movie = movieService.getMovieById(ticket.MovieId);
                    ticket.Projector = context.Projectors
                        .Where(pr => pr.ProjectorId == ticket.ProjectorId)
                        .FirstOrDefault();
                    return ticket;
                }
                else
                {
                    throw new Exception("Ticket not found");
                }
            }
        }
    }
}
