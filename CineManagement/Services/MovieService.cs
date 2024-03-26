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
        public Movie addMovie(Movie movie,bool isShow,int daily,int weekly,int monthly,int soldTicket,long revenue)
        {
            using( var context = new CinemaManagementContext())
            {
                try
                {
                    Movie mv = new Movie
                    {
                        MovieName = movie.MovieName,
                        DirectorId = movie.DirectorId,
                        ReleaseYear = movie.ReleaseYear,
                        Poster = movie.Poster,
                        ImdbRating = movie.ImdbRating,
                        Certification = movie.Certification,
                        Duration = movie.Duration,

                    };
                    var movi = context.Movies.Add(mv);
                    context.SaveChanges();
                    if(movi == null)
                    {
                        throw new Exception("Không thể lưu phim");
                    } else
                    {
           
                            foreach (var actor in movie.Actors)
                            {
                                actor.Movies.Add(mv);
                            }
                            foreach (var gen in movie.Genres)
                            {
                                gen.Movies.Add(mv);
                            }
                            context.SaveChanges();
                            MovieInfo mi = new MovieInfo
                            {
                                MovieId = mv.MovieId,
                                IsSelling = isShow,
                                DailyShowtime = daily,
                                WeeklyShowtime = weekly,
                                MonthlyShowtime = monthly,
                                SoldTicket = soldTicket,
                                TicketRevenue = revenue
                            };
                            context.MovieInfos.Add(mi);
                            context.SaveChanges();
                            return mv;
                      
                    }
                } catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool addMovieInfo(MovieInfo movieInfo) {
            using(var context = new CinemaManagementContext())
            {
                try
                {
                    var info = context.MovieInfos.Add(movieInfo);
                    context.SaveChanges();
                    if (info == null)
                    {
                        throw new Exception("Thông tin thêm bị lỗi");
                    } else return true;
                } catch(Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}