using CineManagement.Models;
using CineManagement.Services;

namespace CineManagement.ViewModels
{
    class HomeViewModel : ViewModelBase
    {
        MovieService ms = new MovieService();

        public List<Movie> Movies { get; set; }
        public List<Movie> BannerMovies { get; set; }
        public List<Movie> BestSellingMovies { get; set; }
        public List<Movie> CurrentlyInCinesMovies { get; set; }

        public Movie SelectedMovie { get => _selectedMovie; set { _selectedMovie = value; OnPropertyChanged(nameof(SelectedMovie)); } }

        private Movie _selectedMovie;

        public HomeViewModel()
        {
            Movies = new List<Movie>();
            BannerMovies = new List<Movie>();
            BestSellingMovies = new List<Movie>();
            CurrentlyInCinesMovies = new List<Movie>();

            List<Movie> movies = ms.getMovies();

            Movies = movies;
            BestSellingMovies = movies.OrderByDescending(movie => movie.MovieInfo?.TicketRevenue).Take(10).ToList();
            CurrentlyInCinesMovies = movies.Where(movie => movie.MovieInfo?.IsSelling == true).ToList();
            BannerMovies = movies.Where(movie => movie.MovieInfo?.IsSelling == true)
                                 .OrderByDescending(movie => movie.MovieInfo?.TicketRevenue)
                                 .Take(5).ToList();
        }
    }
}
