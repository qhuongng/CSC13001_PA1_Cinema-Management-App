using CineManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineManagement.Services
{
    class DirectorService
    {
        public List<Director> getListDirector()
        {
            using(var context = new CinemaManagementContext())
            {
                List<Director> listDirector = context.Directors.ToList();
                if (listDirector == null)
                {
                    throw new Exception("No Director found!");
                } else
                {
                    return listDirector;
                }
            }
        }
    }
}
