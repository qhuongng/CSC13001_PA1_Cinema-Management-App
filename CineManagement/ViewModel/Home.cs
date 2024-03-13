using CineManagement.Models;
using CineManagement.Services;

namespace CineManagement.ViewModel
{
    class Home
    {
        public List<Movie> Movies { get; set; }
        public List<Byte[]> BannerPosters { get; set; }

        MovieService ms = new MovieService();

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
