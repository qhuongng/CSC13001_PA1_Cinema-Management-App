using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using CineManagement.Models;
using CineManagement.ViewModels;

namespace CineManagement.Views
{
    public partial class MovieDetails : UserControl
    {
        public MovieDetails()
        {
            InitializeComponent();
        }

        private void SeatChart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow mw = (MainWindow) Window.GetWindow(this);
            MovieDetailsViewModel vm = (MovieDetailsViewModel) mw.MovieDetailsView.DataContext;

            List<Seat> selectedSeats = SeatChart.SelectedItems.Cast<Seat>().ToList();
            vm.SelectedSeats = selectedSeats;
        }

        private void Projectors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SeatChart.SelectedItems.Clear();

            MainWindow mw = (MainWindow) Window.GetWindow(this);
            MovieDetailsViewModel vm = (MovieDetailsViewModel) mw.MovieDetailsView.DataContext;
            List<string> boughtSeats = vm.BoughtSeats;

            vm.Total = 0;

            if (boughtSeats != null)
            {
                foreach (Seat seat in SeatChart.Items)
                {
                    if (boughtSeats.Contains(seat.SeatId))
                    {
                        seat.IsEnabled = false;
                    }
                    else
                    {
                        seat.IsEnabled = true;
                    }
                }
            }
        }
    }

    public class GenresToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(string))
            {
                throw new InvalidOperationException("The target must be a String");
            }

            List<string> genreNames = new List<string>();

            foreach (Genre genre in (List<Genre>)value)
            {
                genreNames.Add(genre.GenreName);
            }
                
            return string.Join(", ", genreNames.ToArray());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
