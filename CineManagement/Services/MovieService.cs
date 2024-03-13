using CineManagement.Models;

namespace CineManagement.Services
{
    public class MovieService
    {
        public Movie getMovieById(int id)
        {
            using(var context = new CinemaManagementContext())
            {
                Movie movie = context.Movies.FirstOrDefault(x => x.MovieId == id);
                if (movie == null)
                {
                    throw new Exception("Movie no found");
                } else
                {
                    movie.Actors = context.Entry(movie).Collection(m => m.Actors).Query().ToList();
                                    
                    return movie;
                }
            }
        }
    }
}
