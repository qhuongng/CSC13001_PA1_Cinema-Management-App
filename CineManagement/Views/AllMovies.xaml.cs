using CineManagement.Models;
using CineManagement.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CineManagement.Views
{
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

        private void Pages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow mw = (MainWindow)Window.GetWindow(this);
            AllMoviesViewModel vm = (AllMoviesViewModel) mw.AllMoviesView.DataContext;
            string selected = (string)Pages.SelectedItem;
            int pageInd = int.Parse(selected);

            vm.SelectedPage = selected;

            if (selected.Equals(vm.PageIndices.Last())) {
                vm.MoviesInPage = vm.Movies.GetRange(pageInd * 9 - 9, vm.Movies.Count - 9 * (pageInd - 1));
            }
            else {
                vm.MoviesInPage = vm.Movies.GetRange(pageInd * 9 - 9, 9);
            }
        }     
    }
}
