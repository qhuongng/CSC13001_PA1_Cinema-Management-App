using CineManagement.Utilities;
using CineManagement.Models;
using CineManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineManagement.Model;
using System.Collections.ObjectModel;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Identity.Client;
using System.Windows.Input;
using System.Windows;

namespace CineManagement.ViewModels
{
    public class MovieDetail
    {
        public int No { get; set; }
        public string Color { get; set; }
        public string MovieName {  get; set; }
        public byte[] MovieImage { get; set; }
        public string TicketSold { get; set; }
        public MovieDetail(int no,string color, string name, byte[] img, int soldticket) 
        {
            No = no;
            Color = color;
            MovieName = name;
            MovieImage = img;
            TicketSold = soldticket.ToString();
        }
    }
    public class HomeVM : ViewModelBase
    {
        private User user;
        private MovieService movieService;
        private List<Movie> movieList;
        private int showingMovies;//tong so phim dang chieu
        private int totalTickets;//tong so ve ban duoc
        private string totalRevenue;//tong doanh thu
        private long topRevenue;//top doanh thu
        private string highestRevenue;
        private int showInDay;
        private int showInWeek;
        private int showInMonth;
        private string userName;

        //binding chart
        private ObservableCollection<string> movieTitles;
        private ObservableCollection<int> movieRevenues;

        private Dictionary<string, int> listRevenues;

        private ObservableCollection<MovieDetail> _movies;

        public int ShowingMovies { get => showingMovies; set { showingMovies = value; OnPropertyChanged(nameof(ShowingMovies)); } }
        public int TotalTickets { get => totalTickets; set { totalTickets = value; OnPropertyChanged(nameof(TotalTickets)); } }
        public string TotalRevenue { get => totalRevenue; set { totalRevenue = value; } }
        public long TopRevenue { get => topRevenue; set { topRevenue = value; } }

        public ObservableCollection<MovieDetail> Movies { get => _movies; set => _movies = value; }
        public int ShowInDay { get => showInDay; set => showInDay = value; }
        public int ShowInWeek { get => showInWeek; set => showInWeek = value; }
        public int ShowInMonth { get => showInMonth; set => showInMonth = value; }
        public string HighestRevenue { get => highestRevenue; set => highestRevenue = value; }
        public string UserName { get => userName; set { userName = value; OnPropertyChanged(nameof(UserName)); } }
        public ObservableCollection<string> MovieTitles { get => movieTitles; set => movieTitles = value; }
        public ObservableCollection<int> MovieRevenues { get => movieRevenues; set => movieRevenues = value; }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Values { get; set; }
        public ICommand UserCommand { get;}
        public User User { get => user; set { user = value; OnPropertyChanged(nameof(User)); } }

        public HomeVM() 
        {
            _movies = new ObservableCollection<MovieDetail>();
            showInDay = 0;
            showInWeek = 0;
            showInMonth = 0;
            topRevenue = 0;
            long temp = 0;
            movieService = new MovieService();
            listRevenues = new Dictionary<string, int>();
            movieList = movieService.getMovies();
            showingMovies = movieList.Count(m => m.MovieInfo.IsSelling);
            foreach (Movie movie in movieList)
            {
                listRevenues.Add(movie.MovieName, ((int)(movie.MovieInfo.TicketRevenue / 1000000000)));
                showInDay += movie.MovieInfo.DailyShowtime;
                showInWeek += movie.MovieInfo.WeeklyShowtime;
                showInMonth += movie.MovieInfo.MonthlyShowtime;
                if (movie.MovieInfo.TicketRevenue > topRevenue) topRevenue = movie.MovieInfo.TicketRevenue;
                temp += movie.MovieInfo.TicketRevenue;
            }
            totalRevenue = (temp / (1000000000)).ToString() + "B";
            highestRevenue = (topRevenue / (1000000000)).ToString() + "B";
            Dictionary<string, int> sortedList = listRevenues.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            movieTitles = new ObservableCollection<string>();//label
            movieRevenues = new ObservableCollection<int>();//series
            for (int i = 0; i < 15; i++)
            {
                movieTitles.Add(sortedList.ElementAt(i).Key);
                movieRevenues.Add(sortedList.ElementAt(i).Value);
            }
            string[] rangercolor = { "red", "green", "blue", "purple", "orange" };
            for (int i = 0; i < 5; i++)
            {
                var data = movieList.Where(x => x.MovieName == movieTitles.ElementAt(i)).Select(x => new { x.MovieName, x.Poster, x.MovieInfo.SoldTicket }).FirstOrDefault();
                _movies.Add(new MovieDetail(i + 1, rangercolor[i], data.MovieName, data.Poster, data.SoldTicket));
            }
            SeriesCollection = new SeriesCollection()
            {
                new ColumnSeries
                {
                    Title="Top 15 Doanh Thu",
                    Values = new ChartValues<int>(movieRevenues)                }
            };

            Labels = movieTitles.ToArray();
            Values = value => value.ToString("N");
        }
        public HomeVM(User user) : this()
        {
            userName = user.UserName;
            MessageBox.Show(UserName);
            UserCommand = new ViewModelCommand(ExecutedLoadProfile);
        }

        private void ExecutedLoadProfile(object obj)
        {
            var profileAmin = new 
        }
    }
}
