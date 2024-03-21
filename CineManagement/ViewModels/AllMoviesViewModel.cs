using CineManagement.Models;
using CineManagement.Services;

namespace CineManagement.ViewModels
{
    class AllMoviesViewModel : ViewModelBase
    {
        MovieService ms = new MovieService();

        public List<Movie> Movies { get; set; }
        public Movie SelectedMovie { get => _selectedMovie; set { _selectedMovie = value; OnPropertyChanged(nameof(SelectedMovie)); } }

        private Movie _selectedMovie;

        public AllMoviesViewModel()
        {
            // load all movies
            Movies = new List<Movie>();
            List<Movie> movies = ms.getMovies();
            Movies = movies;
        }
    }
}
