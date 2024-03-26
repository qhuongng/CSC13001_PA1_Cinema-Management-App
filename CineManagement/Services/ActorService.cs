using CineManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineManagement.Services
{
    public class ActorService
    {
        public List<Actor> getListActor()
        {
            using (var context = new CinemaManagementContext())
            {
                List<Actor> listActor = context.Actors.ToList();
                if (listActor == null)
                {
                    throw new Exception("No Director found!");
                }
                else
                {
                    return listActor;
                }
            }
        }
    }
}
