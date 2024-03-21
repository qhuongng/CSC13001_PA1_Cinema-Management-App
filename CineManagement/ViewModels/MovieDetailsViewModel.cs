using CineManagement.Models;
using CineManagement.Services;

namespace CineManagement.ViewModels
{
    class MovieDetailsViewModel : ViewModelBase
    {
        MovieService ms = new MovieService();

        public Movie SelectedMovie { get => _selectedMovie; set { _selectedMovie = value; OnPropertyChanged(nameof(SelectedMovie)); } }

        private Movie _selectedMovie;

        public MovieDetailsViewModel(Movie selected)
        {
            _selectedMovie = selected;
        }
    }
}
