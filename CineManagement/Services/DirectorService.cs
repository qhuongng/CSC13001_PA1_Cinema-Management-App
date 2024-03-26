using CineManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineManagement.Services
{
    public class DirectorService
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
        public Director getDirectorByName(string name)
        {
            using( var context = new CinemaManagementContext())
            {
                Director director = context.Directors.FirstOrDefault(x => x.DirectorName == name);
                if(director == null)
                {
                    throw new Exception("Không tìm thấy đạo diễn!");
                } else { return director; }
            }
        }
    }
}
