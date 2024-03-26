using CineManagement.Models;
using CineManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace CineManagement.ViewModels
{
    class MovieAddViewModel: ViewModelBase
    {
        private string _movieName;
        private int _duration;
        private string _selectedAge;
        private double _imdbRating;
        private int _releaseYear;
        private bool _checkShow;
        private byte[] poster;
        private string _errorMessage;
        private string _selectedDirector;
        private int _soldTicket;
        private int _dailyShowTime;
        private int _weeklyShowTime;
        private int _monthlyShowTime;
        private long _ticketRevenue;
        private string _fileName;
        private string _listActorArr;
        private string _selectedGenres;
        private List<Actor> _selectedActorList;


        public string MovieName { get => _movieName; set {  _movieName = value; OnPropertyChanged(nameof(MovieName)); } }
        public int Duration { get => _duration; set {  _duration = value; OnPropertyChanged(nameof(Duration)); } }
        public string SelectedAge { get => _selectedAge; set {  _selectedAge = value; OnPropertyChanged(nameof(SelectedAge)); } }
        public double ImdbRating { get => _imdbRating; set {  _imdbRating = value; OnPropertyChanged(nameof(ImdbRating)); } }
        public int ReleaseYear { get => _releaseYear; set {  _releaseYear = value; OnPropertyChanged(nameof(ReleaseYear)); } }
        public bool CheckShow { get => _checkShow; set {  _checkShow = value; OnPropertyChanged(nameof(CheckShow)); } }
        public string ErrorMessage { get => _errorMessage; set {  _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); } }
        public string SelectedDirector { get => _selectedDirector; set {  _selectedDirector = value; OnPropertyChanged(nameof(SelectedDirector)); } }
        public int SoldTicket { get => _soldTicket; set {  _soldTicket = value; OnPropertyChanged(nameof(SoldTicket)); } }
        public int DailyShowTime { get => _dailyShowTime; set {  _dailyShowTime = value; OnPropertyChanged(nameof(DailyShowTime)); } }
        public int WeeklyShowTime { get => _weeklyShowTime; set {  _weeklyShowTime = value; OnPropertyChanged(nameof(WeeklyShowTime)); } }
        public int MonthlyShowTime { get => _monthlyShowTime; set {  _monthlyShowTime = value; OnPropertyChanged(nameof(MonthlyShowTime)); } }
        public long TicketRevenue { get => _ticketRevenue; set {  _ticketRevenue = value; OnPropertyChanged(nameof(TicketRevenue)); } }
        public string FileName { get => _fileName; set {  _fileName = value; OnPropertyChanged(nameof(FileName)); } }
        public string SelectedGenres { get => _selectedGenres; set { _selectedGenres = value; OnPropertyChanged(nameof(SelectedGenres)); } }
        public string ListActorArr { get => _listActorArr; 
            set {
                _listActorArr = value;
                OnPropertyChanged(nameof(ListActorArr)); }
        }

        public List<Actor> SelectedActorList { get => _selectedActorList; set { _selectedActorList = value;
                ListActorArr = "";
                foreach (var item in SelectedActorList)
                {
                    ListActorArr += item.ActorName + "; ";

                };
                OnPropertyChanged(nameof(SelectedActorList)); } }

        public MovieService movieManage;
        public AgeRatingService ageManage;
        public DirectorService directorManage;
        public ActorService actorManage;
        public GenreService genreManage;
   
        public ObservableCollection<string> AgeRating { get; set; }
        public ObservableCollection<bool> IsShow { get; set; }
        public ObservableCollection<string> Directors { get; set; }
        public ObservableCollection<Actor> Actors { get; set; }

        public ObservableCollection<string> Genres { get; set; }

        public ICommand AddCommand { get; set; }
        public ICommand GetImageCommand { get; set; }
        
        public Movie currentMovie { get; set; }
        public Window window { get; set; }

        public MovieAddViewModel(Window currentWindow)
        {
            window = currentWindow;
            //khởi tạo các service
            movieManage = new MovieService();
            ageManage = new AgeRatingService();
            directorManage = new DirectorService();
            actorManage = new ActorService();
            genreManage = new GenreService();
            

            //
            _selectedActorList = new List<Actor>();
            _listActorArr = "";
            _fileName = "";
            currentMovie = new Movie();
            //
            //khởi tạo các giá trị cho các list
            // 
            try
            {
                AgeRating = new ObservableCollection<string>(ageManage.getListAgeRating().Select(x => x.AgeId));
                IsShow = new ObservableCollection<bool> { true, false };
                Actors = new ObservableCollection<Actor>(actorManage.getListActor());
                Directors = new ObservableCollection<string>(directorManage.getListDirector().Select(x => x.DirectorName));
                Genres = new ObservableCollection<string>(genreManage.getListGenres().Select(x => x.GenreName));
           
            } catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

            //khởi tạo command
            AddCommand = new ViewModelCommand(ExecutedAddCommand);
            GetImageCommand = new ViewModelCommand(ExecutedGetImageCommand);
        }
        private void ExecutedGetImageCommand(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp|All Files (*.*)|*.*";
            openFileDialog.Title = "Select image file(s)...";
            openFileDialog.Multiselect = false;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            var check = openFileDialog.ShowDialog();
            if (check == DialogResult.OK)
            {

                FileName = openFileDialog.FileName.Split('\\').Last();
            

                try
                {
                    poster = File.ReadAllBytes(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    ErrorMessage = "Lỗi khi đọc tệp: " + ex.Message;
                }
            }
        }

        private void ExecutedAddCommand(object obj)
        {
            if (string.IsNullOrWhiteSpace(_movieName))
            {
                ErrorMessage = "Điền vào các vùng còn trống!";
            } else
            {
                ErrorMessage = "";
                try
                {
                    Director temp = directorManage.getDirectorByName(_selectedDirector);
                    AgeRating rating = ageManage.GetAgeRating(_selectedAge.Replace(" ", ""));
                    Genre genre = genreManage.getGenreByName(_selectedGenres);
                    List<Genre> genres = new List<Genre>();
                    genres.Add(genre);
                    Movie newMovie = new Movie() { MovieName = _movieName, DirectorId = temp.DirectorId, Duration = _duration, ReleaseYear = _releaseYear, ImdbRating = _imdbRating, Poster = poster, Certification = _selectedAge };
                    newMovie.Director = temp;
                    newMovie.CertificationNavigation = rating;
                    newMovie.Actors = _selectedActorList.ToArray();
                    newMovie.Genres = genres.ToArray();

                    bool checkadd = movieManage.addMovie(newMovie);
                    if (checkadd)
                    {
                        MovieInfo newInfo = new MovieInfo() { MovieId = newMovie.MovieId, IsSelling = _checkShow, DailyShowtime = _dailyShowTime, WeeklyShowtime = _weeklyShowTime, MonthlyShowtime = _monthlyShowTime, SoldTicket = _soldTicket, TicketRevenue = _ticketRevenue };
                        newInfo.Movie = newMovie;

                        movieManage.addMovieInfo(newInfo);
                    }
                    currentMovie = newMovie;
                    window.DialogResult = true;
                    window.Close();
                } catch (Exception ex)
                {
                    ErrorMessage += ex.Message;
                }
               

            }
        }
    }
}
