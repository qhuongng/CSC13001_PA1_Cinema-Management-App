using CineManagement.ViewModels;
using CineManagement.Models;
using System.Windows;
using System.Windows.Controls;

namespace CineManagement.Views
{
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
            DataContext = new HomeViewModel();
        }

        private void movieList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow mw = (MainWindow) Window.GetWindow(this);
            Movie selected = (Movie) movieList.SelectedItem;
            mw.MovieDetailsView.DataContext = new MovieDetailsViewModel(selected);
            mw.MovieDetailsView.SeatChart.UnselectAll();
            mw.HideAllExcept(mw.Root, mw.MovieDetailsView);
        }
    }
}
