using CineManagement.Models;
using CineManagement.Services;
using System.Collections.ObjectModel;

namespace CineManagement.ViewModels
{
    class AllMoviesViewModel : ViewModelBase
    {
        MovieService ms = new MovieService();

        public ObservableCollection<string> PageIndices { get; set; }
        public List<Movie> Movies { get; set; }
        public List<Movie> MoviesInPage { get => _moviesInPage; set { _moviesInPage = value; OnPropertyChanged(nameof(MoviesInPage)); } }
        public Movie SelectedMovie { get => _selectedMovie; set { _selectedMovie = value; OnPropertyChanged(nameof(SelectedMovie)); } }
        public string SelectedPage { get => _selectedPage; set { _selectedPage = value; OnPropertyChanged(nameof(SelectedPage)); } }

        private Movie _selectedMovie;
        private string _selectedPage;
        private List<Movie> _moviesInPage;

        public AllMoviesViewModel()
        {
            Movies = new List<Movie>();
            List<Movie> movies = ms.getMovies();
            Movies = movies;

            PageIndices = new ObservableCollection<string>();
            PopulatePager();
            _moviesInPage = Movies.GetRange(0, 9);
            _selectedPage = "1";
        }

        private void PopulatePager()
        {
            int n = Movies.Count / 9;

            if (Movies.Count % 9 > 0)
            {
                n++;
            }

            for (int i = 1; i <= n; i++)
            {
                PageIndices.Add($"{i}");
            }
        }
    }
}
