using CineManagement.Models;
using CineManagement.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

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
            MainWindow mw = (MainWindow) Window.GetWindow(this);
            MainWindowViewModel vm = (MainWindowViewModel) mw.DataContext;
            Movie selected = (Movie) movieList.SelectedItem;
            User? currentUser = null;

            if (vm.CurrentUser != null)
            {
                currentUser = vm.CurrentUser;
            }

            mw.MovieDetailsView.DataContext = new MovieDetailsViewModel(selected, currentUser);
            mw.MovieDetailsView.SeatChart.UnselectAll();
            mw.HideAllExcept(mw.Root, mw.MovieDetailsView);
        }

        private void Pages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow mw = (MainWindow) Window.GetWindow(this);
            AllMoviesViewModel vm = (AllMoviesViewModel) mw.AllMoviesView.DataContext;
            string selected = (string) Pages.SelectedItem;
            int pageInd = int.Parse(selected);

            vm.SelectedPage = selected;

            if (selected.Equals(vm.PageIndices.Last())) {
                vm.MoviesInPage = vm.Movies.GetRange(pageInd * 9 - 9, vm.Movies.Count - 9 * (pageInd - 1));
            }
            else {
                vm.MoviesInPage = vm.Movies.GetRange(pageInd * 9 - 9, 9);
            }
        }

        public static T FindChild<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child != null && child is T)
                {
                    return (T)child;
                }

                var childOfChild = FindChild<T>(child);

                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }

            return null;
        }

        private void HandleFail(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show("lmao " + e.ErrorException);
        }

        private void ElementPopup_Opened(object sender, EventArgs e)
        {
            Popup popup = sender as Popup;
            MediaElement trailer = FindChild<MediaElement>(popup.Child);
            trailer.Source = new Uri("pack://siteoforigin:,,,/Clips/trailer.mp4");

            trailer.Play();
        }

        private void ElementPopup_Closed(object sender, EventArgs e)
        {
            Popup popup = sender as Popup;
            MediaElement trailer = FindChild<MediaElement>(popup.Child);

            trailer.Stop();
        }
    }
}
