using CineManagement.Models;
using CineManagement.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CineManagement.Views
{
    /// <summary>
    /// Interaction logic for AllMovies.xaml
    /// </summary>
    public partial class AllMovies : UserControl
    {
        public AllMovies()
        {
            InitializeComponent();
        }

        private void movieList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow mw = (MainWindow)Window.GetWindow(this);
            Movie selected = (Movie)movieList.SelectedItem;
            mw.MovieDetailsView.DataContext = new MovieDetailsViewModel(selected);
            mw.MovieDetailsView.SeatChart.UnselectAll();
            mw.HideAllExcept(mw.Root, mw.MovieDetailsView);
        }
    }
}
