using CineManagement.Models;
using CineManagement.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace CineManagement.ViewModels
{
    class MovieUpdateViewModel :ViewModelBase
    {

        private byte[] _poster;
        private string _movieName;
        private int _duration;
        private string _selectedAge;
        private double _imdbRating;
        private int _releaseYear;
        private bool _checkShow;
        private string _errorMessage;
        private string _successMessage;

        public byte[] Poster { get => _poster; set { _poster = value;OnPropertyChanged(nameof(Poster)); } }
        public string MovieName { get => _movieName; set { _movieName = value;OnPropertyChanged(nameof(MovieName)); } }
        public int Duration { get => _duration; set { _duration = value;OnPropertyChanged(nameof(Duration)) ; } }
        public string SelectedAge { get => _selectedAge; set { _selectedAge = value; OnPropertyChanged(nameof(SelectedAge)); } }
        public double ImdbRating { get => _imdbRating; set { _imdbRating = value; OnPropertyChanged(nameof(ImdbRating)); } }
        public int ReleaseYear { get => _releaseYear; set { _releaseYear = value; OnPropertyChanged(nameof(ReleaseYear)); } }
        public bool CheckShow { get => _checkShow; set { _checkShow = value; OnPropertyChanged(nameof(CheckShow)); } }
        public string ErrorMessage { get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); } }
        public string SuccessMessage { get => _successMessage; set { _successMessage = value; OnPropertyChanged(nameof(SuccessMessage)); } }

        public ObservableCollection<string> AgeRating { get; set; }
        public ObservableCollection<bool> IsShow {get;set; }
        public ICommand UpdateCommand { get; }

        public Movie movie;
        public Window window;
        public MovieService movieService;
        public AgeRatingService ageRatingService;

        public MovieUpdateViewModel(Movie currentMovie, Window currentWindow)
        {
            movieService = new MovieService();
            ageRatingService = new AgeRatingService();
            //
            AgeRating = new ObservableCollection<string>(ageRatingService.getListAgeRating().Select(x => x.AgeId));
            IsShow = new ObservableCollection<bool>();
            IsShow.Add(true); IsShow.Add(false);
            //
            movie = currentMovie;
            window = currentWindow;
            Poster = movie.Poster;
            _movieName = movie.MovieName;
            _duration = movie.Duration;
            _selectedAge = movie.Certification;
            _imdbRating = movie.ImdbRating;
            _releaseYear = movie.ReleaseYear;
            _checkShow = movie.MovieInfo.IsSelling;
            _errorMessage = "";
            _successMessage = "";
            //
            UpdateCommand = new ViewModelCommand(ExecutedUpdateCommand);
        }

        private void ExecutedUpdateCommand(object obj)
        {
            if (string.IsNullOrEmpty(_movieName) || string.IsNullOrEmpty(_duration.ToString()) || string.IsNullOrWhiteSpace(_duration.ToString())
                || string.IsNullOrEmpty(_imdbRating.ToString()) || string.IsNullOrWhiteSpace(_imdbRating.ToString()) 
                || string.IsNullOrEmpty(_releaseYear.ToString()) || string.IsNullOrWhiteSpace(_releaseYear.ToString()))
            {
                ErrorMessage = "* Please fill all blank!";
                SuccessMessage = "";
            } else if(!int.TryParse(_duration.ToString(), out int duration))
            {
                ErrorMessage = "* Duration must be a valid number!";
                SuccessMessage = "";
            } else if(!float.TryParse(_imdbRating.ToString(), out float imb))
            {
                ErrorMessage = "* IMDB must be a valid number!";
                SuccessMessage = "";
            } else if(!int.TryParse(_releaseYear.ToString(), out int releaseYear))
            {
                ErrorMessage = "* Release Year must be a valid number!";
                SuccessMessage = "";
            } else if(_movieName == movie.MovieName && _duration == movie.Duration && 
                _imdbRating == movie.ImdbRating && _releaseYear == movie.ReleaseYear &&
                _checkShow == movie.MovieInfo.IsSelling && _selectedAge == movie.Certification)
            {
                ErrorMessage = "* Nothing to update!";
                SuccessMessage = "";
            } else
            {
                movie.MovieName = _movieName;
                movie.Duration = _duration;
                movie.ImdbRating = _imdbRating;
                movie.Certification = _selectedAge;
                movie.ReleaseYear = _releaseYear;
                movie.MovieInfo.IsSelling = _checkShow;
                try
                {
                    bool checkUpdate = movieService.updateMovie(movie);
                    if(checkUpdate )
                    {
                        SuccessMessage = "Update successfully";
                        window.DialogResult = true;
                        window.Close();
                    }
                } catch (Exception ex)
                {
                    ErrorMessage =  "* " + ex.Message;
                    SuccessMessage = "";
                }
                
                
            }
        }
    }
}
