using CineManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineManagement.Services
{
    public class TicketService
    {
        private MovieService movieService = new MovieService();
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
