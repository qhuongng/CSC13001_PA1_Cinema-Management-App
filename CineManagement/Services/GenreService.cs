using CineManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineManagement.Services
{
    public class GenreService
    {
        public List<Genre> getListGenres()
        {
            using (var context = new CinemaManagementContext())
            {
                List<Genre> genreList = context.Genres.ToList();
                if (genreList == null)
                {
                    throw new Exception("No AgeRating found.");
                }
                else
                {
                    return genreList;
                }
            }
        }
        public Genre getGenreByName(string name)
        {
            using(var context = new CinemaManagementContext())
            {
                Genre genre = context.Genres.FirstOrDefault(x => x.GenreName == name);
                if(genre == null)
                {
                    throw new Exception($"Could not find {name}");
                } else return genre;
            }
        }
    }
}
