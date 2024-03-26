                                                                                                                                                                                                                                                                using CineManagement.Model;
using CineManagement.Models;
using CineManagement.Services;
using CineManagement.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CineManagement.ViewModels
{
    public class MovieList
    {
        public int Number { get; set; }
        public char Character { get; set; }
        public Movie MovieDetail { get; set; }

        public MovieList(int num, char character,Movie movie) 
        {
            Number = num;
            Character = character;
            MovieDetail = movie;
        }
    }
    class MovieControlViewModel : ViewModelBase
    {
        private int _totalMovie;
        private ObservableCollection<MovieList> _movies;
        private MovieList _selectedMovie;
        public MovieService movieService;

        public ICommand addCommand { get; }
        public ICommand updateCommand { get; }
        public ICommand deleteCommand { get; }
        public int TotalMovie { get => _totalMovie; set { _totalMovie = value;OnPropertyChanged(nameof(TotalMovie)); } }
        public ObservableCollection<MovieList> Movies { get => _movies; set { _movies = value; OnPropertyChanged(nameof(Movies)); } }
        public MovieList SelectedMovie { get => _selectedMovie; set { _selectedMovie = value; OnPropertyChanged(nameof(SelectedMovie)); } }

        public MovieControlViewModel()
        {
            movieService = new MovieService();
            _totalMovie = 0;
            addCommand = new ViewModelCommand(executedAddCommand);
            updateCommand = new ViewModelCommand(executedUpdateCommand);
            deleteCommand = new ViewModelCommand(executedDeleteCommand);
            try
            {
                var movie = movieService.getMovies();
                _movies = new ObservableCollection<MovieList>();
                _totalMovie += movie.Count;
                int count = 1;
                char cha;
                foreach(Movie movi in movie)
                {
                    cha = char.ToUpper(movi.MovieName[0]);
                    _movies.Add(new MovieList(count,cha,movi));
                    count++;
                }


            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void executedDeleteCommand(object obj)
        {
            if (_selectedMovie != null)
            {
                try
                {
                    bool checkdel = movieService.deleteMovieById(_selectedMovie.MovieDetail.MovieId);
                    if (checkdel)
                    {
                        _movies.Remove(_selectedMovie);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }   
        }

        private void executedUpdateCommand(object obj)
        {
            if(_selectedMovie != null)
            {
                var UpdateScreen = new MovieUpdate(_selectedMovie.MovieDetail);
                UpdateScreen.ShowDialog();
                if(UpdateScreen.DialogResult == true)
                {
                    _selectedMovie.MovieDetail = UpdateScreen.updateMovie;
                }
            }
        }

        private void executedAddCommand(object obj)
        {
            var AddScreen = new MovieAdd();
            AddScreen.ShowDialog();
            if(AddScreen.DialogResult == true)
            {
                _movies.Add(new MovieList(_movies.Count + 1, char.ToUpper(AddScreen.newMovie[0].MovieName[0]),AddScreen.newMovie[0]));
            }
        }
    }
}
