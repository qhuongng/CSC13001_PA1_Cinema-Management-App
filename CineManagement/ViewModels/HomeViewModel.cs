using CineManagement.Models;
using CineManagement.Services;
using System.ComponentModel;
using System.Windows.Threading;

namespace CineManagement.ViewModels
{
    class HomeViewModel : ViewModelBase
    {
        MovieService ms = new MovieService();

        public List<Movie> Movies { get; set; }
        public List<byte[]> BannerPosters { get; set; }
        public Movie SelectedMovie { get => _selectedMovie; set { _selectedMovie = value; OnPropertyChanged(nameof(SelectedMovie)); } }

        private Movie _selectedMovie;

        public HomeViewModel()
        {
            // load all movies
            Movies = new List<Movie>();
            List<Movie> movies = ms.getMovies();
            Movies = movies;

            // load FlipView posters
            BannerPosters = new List<byte[]>();
            for (int i = 0; i < 5; i++)
            {
                BannerPosters.Add(Movies[i].Poster);
            }
        }
    }
}
