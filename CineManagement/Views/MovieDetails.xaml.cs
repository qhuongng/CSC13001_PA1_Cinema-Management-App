using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using CineManagement.Models;

namespace CineManagement.Views
{
    public partial class MovieDetails : UserControl
    {
        public ObservableCollection<string> SeatIndices { get; set; }

        public MovieDetails()
        {
            InitializeComponent();

            SeatIndices = new ObservableCollection<string>();
            PopulateSeatChart();
            SeatChart.ItemsSource = SeatIndices;
        }

        private void PopulateSeatChart()
        {
            char startChar = 'A';
            int maxRows = 10;
            int maxColumns = 8; // Assuming 80 seats

            for (int row = 1; row <= maxRows; row++)
            {
                for (int column = 1; column <= maxColumns; column++)
                {
                    char currentChar = (char)(startChar + row - 1);
                    SeatIndices.Add($"{currentChar}{column}");
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
