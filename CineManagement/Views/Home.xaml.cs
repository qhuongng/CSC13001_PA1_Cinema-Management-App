using CineManagement.ViewModels;
using CineManagement.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Globalization;

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

        private void posterBanner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow mw = (MainWindow) Window.GetWindow(this);
            Movie selected = (Movie) posterBanner.SelectedItem;
            mw.MovieDetailsView.DataContext = new MovieDetailsViewModel(selected);
            mw.MovieDetailsView.SeatChart.UnselectAll();
            mw.HideAllExcept(mw.Root, mw.MovieDetailsView);
        }
    }

    public class ItemsLimiter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int count;

            if (Int32.TryParse((string)parameter, out count))
            {
                return ((IEnumerable<object>)value).Take(count);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
