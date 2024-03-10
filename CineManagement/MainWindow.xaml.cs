using MahApps.Metro.Controls;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace CineManagement
{
    public partial class MainWindow : MetroWindow
    {
        public ObservableCollection<Film> Films { get; set; }
        public bool IsPopupOpen { get; set; }   

        public MainWindow()
        {
            InitializeComponent();

            filmList.Items.Clear();
            filmList.ItemsSource = null;

            Films = new ObservableCollection<Film>();
            Films.Add(new Film { FilmName = "Film 1", FilmDescription = "Description 1", Thumbnail = "/Images/film1.jpg" });
            Films.Add(new Film { FilmName = "Film 2", FilmDescription = "Description 2", Thumbnail = "/Images/film2.jpg" });
            Films.Add(new Film { FilmName = "Film 3", FilmDescription = "Description 3", Thumbnail = "/Images/film3.jpg" });
            Films.Add(new Film { FilmName = "Film 4", FilmDescription = "Description 4", Thumbnail = "/Images/film4.jpg" });
            Films.Add(new Film { FilmName = "Film 5", FilmDescription = "Description 5", Thumbnail = "/Images/film5.jpg" });
            Films.Add(new Film { FilmName = "Film 6", FilmDescription = "Description 6", Thumbnail = "/Images/film6.jpg" });
            Films.Add(new Film { FilmName = "Film 7", FilmDescription = "Description 7", Thumbnail = "/Images/film7.jpg" });
            Films.Add(new Film { FilmName = "Film 8", FilmDescription = "Description 8", Thumbnail = "/Images/film8.jpg" });

            filmList.ItemsSource = Films;

            DataContext = this;
        }
    }

    public class Film
    {
        public string FilmName { get; set; }
        public string FilmDescription { get; set; }
        public string Thumbnail { get; set; }
    }
}