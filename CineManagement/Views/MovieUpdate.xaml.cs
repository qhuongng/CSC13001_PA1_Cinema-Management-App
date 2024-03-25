using CineManagement.Models;
using CineManagement.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace CineManagement.Views
{
    /// <summary>
    /// Interaction logic for MovieDetail.xaml
    /// </summary>
    public partial class MovieUpdate : Window
    {
        private static readonly Regex _regex = new Regex(@"^[0-9.]+$");
        public Movie updateMovie { get; set; }
        public MovieUpdate(Movie currentMovie)
        {
            InitializeComponent();
            var context = new MovieUpdateViewModel(currentMovie, this);
            this.DataContext = context;
            updateMovie = context.movie;
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
    }
}
