using System.Windows.Controls;
using CineManagement.Models;
using CineManagement.Services;

namespace CineManagement
{
    public partial class Home : UserControl
    {
        List<Movie> movies = new List<Movie>();
        List<Byte[]> posters = new List<Byte[]>();

        MovieService ms = new MovieService();

        public Home()
        {
            InitializeComponent();
            DataContext = this;
            
            for (int i = 0; i < 8; i++)
            {
                movies.Add(ms.getMovieById(i + 1));
                posters.Add(ms.getMovieById(i + 1).Poster);
            }

            movieList.ItemsSource = movies;
            posterBanner.ItemsSource = posters;
        }
    }
}
