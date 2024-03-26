using CineManagement.Models;
using CineManagement.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace CineManagement.Views
{
    public partial class AddShowtime : Window
    {
        public List<Projector> addShowtime { get; set; }

        public AddShowtime()
        {
            InitializeComponent();
            ShowtimeAddViewModel viewModel = new ShowtimeAddViewModel(this);
            DataContext = viewModel;
            addShowtime = viewModel.newShowtime;
        }
    }
}
