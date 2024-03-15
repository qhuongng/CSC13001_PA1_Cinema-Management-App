using CineManagement.Models;
using CineManagement.Services;
using System.ComponentModel;
using System.Windows.Threading;

namespace CineManagement.ViewModels
{
    class HomeViewModel : ViewModelBase
    {
        public HomeViewModel() {
            
        }
        private bool showPopup;
        private DispatcherTimer timer;
        MovieService ms = new MovieService();

        public List<Movie> Movies { get; set; }
        public List<byte[]> BannerPosters { get; set; }


        public void LoadAllMovies()
        {
            Movies = new List<Movie>();

            List<Movie> movies = ms.getMovies();
            Movies = movies;
        }

        public void LoadBannerPosters()
        {
            BannerPosters = new List<byte[]>();

            for (int i = 0; i < 5; i++)
            {
                BannerPosters.Add(Movies[i].Poster);
            }
        }
    }
}
