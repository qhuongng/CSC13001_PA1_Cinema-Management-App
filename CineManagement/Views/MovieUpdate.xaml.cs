using CineManagement.Models;
using CineManagement.ViewModels;
using System.Windows;

namespace CineManagement.Views
{
    /// <summary>
    /// Interaction logic for MovieDetail.xaml
    /// </summary>
    public partial class MovieUpdate : Window
    {
        public Movie updateMovie { get; set; }
        public MovieUpdate(Movie currentMovie)
        {
            InitializeComponent();
            var context = new MovieUpdateViewModel(currentMovie, this);
            this.DataContext = context;
            updateMovie = context.movie;
        }
            
    }
}
