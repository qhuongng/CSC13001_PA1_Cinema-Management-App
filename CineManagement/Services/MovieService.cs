using CineManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CineManagement.Services
{
    public class MovieService
    {
        public List<Movie> getMovies()
        {
            using (var context = new CinemaManagementContext())
            {
                List<Movie> movies = context.Movies.Include(movie => movie.Director)
                                                   .Include(movie => movie.MovieInfo)
                                                   .ToList();

                if (movies == null)
                {
                    throw new Exception("No movie found.");
                }
                else
                {
                    foreach(Movie movie in movies)
                    {
                        movie.Actors = context.Entry(movie).Collection(m => m.Actors).Query().ToList();
                        movie.Genres = context.Entry(movie).Collection(m => m.Genres).Query().ToList();
                        movie.MovieInfo = context.MovieInfos
                            .Where(mi => mi.MovieId == movie.MovieId)
                            .FirstOrDefault();
                    }
                    return movies;
                }
            }
        }
        public Movie getMovieById(int id)
        {
            using (var context = new CinemaManagementContext())
            {
                Movie movie = context.Movies.FirstOrDefault(x => x.MovieId == id);

                if (movie == null)
                {
                    throw new Exception("Movie not found.");
                }
                else
                {
                    movie.Actors = context.Entry(movie).Collection(m => m.Actors).Query().ToList();
                    movie.Genres = context.Entry(movie).Collection(m => m.Genres).Query().ToList();
                    movie.MovieInfo = context.MovieInfos
                        .Where(mi => mi.MovieId == movie.MovieId)
                        .FirstOrDefault();

                    return movie;
                }
            }
        }
        public bool deleteMovieById(int id)
        {
            using (var context = new CinemaManagementContext())
            {
                Movie movie = context.Movies.FirstOrDefault(m => m.MovieId == id);
                if(movie == null)
                {
                    throw new Exception("Movie not found.");
                } else
                {
                    var movieInfo = context.MovieInfos.FirstOrDefault(m => m.MovieId == movie.MovieId);
                    context.MovieInfos.Remove(movieInfo);
                    context.Movies.Remove(movie);
                    context.SaveChanges();
                    return true;
                }
            }
        }
        public bool updateMovie(Movie movie)
        {
            using(var context = new CinemaManagementContext())
            {
                Movie oldMovie = context.Movies.FirstOrDefault(m => m.MovieId.Equals(movie.MovieId));
                oldMovie.MovieInfo = context.MovieInfos
                        .Where(mi => mi.MovieId == oldMovie.MovieId)
                        .FirstOrDefault();
                if (oldMovie == null)
                {
                    throw new Exception("Movie not found.");
                } else
                {
                    oldMovie.MovieName = movie.MovieName;
                    oldMovie.MovieInfo.IsSelling = movie.MovieInfo.IsSelling;
                    oldMovie.Certification = movie.Certification;
                    oldMovie.ReleaseYear = movie.ReleaseYear;
                    oldMovie.Duration = movie.Duration;
                    oldMovie.ImdbRating = movie.ImdbRating;
                    context.SaveChanges();
                    return true;
                }
            }
        }
    }
}