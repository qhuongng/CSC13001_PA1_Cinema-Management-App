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

namespace CineManagement.ViewModels
{
    class MovieDetail
    {
        public string CountryName {  get; set; }
        public byte[] Flag { get; set; }
        public string Price { get; set; }
        public MovieDetail(string name, byte[] img, int soldticket) 
        {
            CountryName = name;
            Flag = img;
            Price = soldticket.ToString();
        }
    }
    public class HomeVM : ViewModelBase
    {

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
        private string movieName;

        //binding chart
        private ObservableCollection<string> movieTitles;
        private ObservableCollection<int> movieRevenues;

        private Dictionary<string, int> listRevenues;
        private SeriesCollection seriesCollection;

        private ObservableCollection<MovieDetail> _movies;

        public int ShowingMovies { get => showingMovies; set { showingMovies = value; OnPropertyChanged(nameof(ShowingMovies)); } }
        public int TotalTickets { get => totalTickets; set { totalTickets = value; OnPropertyChanged(nameof(TotalTickets)); } }
        public string TotalRevenue { get => totalRevenue; set { totalRevenue = value; } }
        public long TopRevenue { get => topRevenue; set { topRevenue = value; } }

        internal ObservableCollection<MovieDetail> Movies { get => _movies; set => _movies = value; }
        public int ShowInDay { get => showInDay; set => showInDay = value; }
        public int ShowInWeek { get => showInWeek; set => showInWeek = value; }
        public int ShowInMonth { get => showInMonth; set => showInMonth = value; }
        public string HighestRevenue { get => highestRevenue; set => highestRevenue = value; }
        public string MovieName { get => movieName; set => movieName = value; }
        public ObservableCollection<string> MovieTitles { get => movieTitles; set => movieTitles = value; }
        public ObservableCollection<int> MovieRevenues { get => movieRevenues; set => movieRevenues = value; }

        public HomeVM()
        {
            showInDay = 0;
            showInWeek = 0;
            showInMonth = 0;
            topRevenue = 0;
            long temp = 0;
            movieService = new MovieService();
            listRevenues = new Dictionary<string, int>();
            movieList = movieService.getMovies();
            showingMovies = movieList.Count(m => m.MovieInfo.IsSelling);
            foreach(Movie movie in movieList)
            {
                listRevenues.Add(movie.MovieName, ((int)(movie.MovieInfo.TicketRevenue / 1000000000)));
                showInDay += movie.MovieInfo.DailyShowtime;
                showInWeek += movie.MovieInfo.WeeklyShowtime;
                showInMonth += movie.MovieInfo.MonthlyShowtime;
                if (movie.MovieInfo.TicketRevenue > topRevenue) topRevenue = movie.MovieInfo.TicketRevenue;
                temp += movie.MovieInfo.TicketRevenue;
               // Movies.Add(new MovieDetail(movie.MovieName,movie.Poster,movie.MovieInfo.SoldTicket));
            }
            totalRevenue = (temp/(1000000000)).ToString() + "B";
            highestRevenue = (topRevenue/(1000000000)).ToString() + "B";
            Dictionary<string,int> sortedList = listRevenues.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            movieTitles = new ObservableCollection<string>();//label
            movieRevenues = new ObservableCollection<int>();//series
            for(int i = 0;i < 15;i++)
            {
                movieTitles.Add(sortedList.ElementAt(i).Key);
                movieRevenues.Add(sortedList.ElementAt(i).Value);
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

        public SeriesCollection SeriesCollection {  get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Values {  get; set; }   


    }
}
