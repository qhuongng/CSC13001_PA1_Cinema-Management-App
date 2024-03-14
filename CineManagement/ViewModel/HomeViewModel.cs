using CineManagement.Models;
using CineManagement.Services;
using System.ComponentModel;
using System.Windows.Threading;

namespace CineManagement.ViewModel
{
    class HomeViewModel : INotifyPropertyChanged
    {
        private bool showPopup;
        private DispatcherTimer timer;

        MovieService ms = new MovieService();

        public List<Movie> Movies { get; set; }
        public List<Byte[]> BannerPosters { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void LoadAllMovies() {
            Movies = new List<Movie>();

            List<Movie> movies = ms.getMovies();
            Movies = movies;
        }

        public void LoadBannerPosters()
        {
            BannerPosters = new List<Byte[]>();

            for (int i = 0; i < 5; i++)
            {
                BannerPosters.Add(Movies[i].Poster);
            }
        }
    }
}
