using CineManagement.Models;
using CineManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CineManagement.Views
{
    /// <summary>
    /// Interaction logic for MovieAdd.xaml
    /// </summary>
    public partial class MovieAdd : Window
    {
        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        private MovieAddViewModel _viewModel;
        public Movie newMovie { get; set; }
        public MovieAdd()
        {
            InitializeComponent();
            _viewModel = new MovieAddViewModel(this);
            this.DataContext = _viewModel;
            newMovie = _viewModel.currentMovie;
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

        private void Actor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MovieAddViewModel vm = (MovieAddViewModel)this.DataContext;

            List<Actor> selectedActors = ActorList.SelectedItems.Cast<Actor>().ToList();
            vm.SelectedActorList = selectedActors;
        }
    }
}
