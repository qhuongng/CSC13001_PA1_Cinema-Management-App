using CineManagement.Models;
using CineManagement.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CineManagement.ViewModels
{
    internal class ShowtimeAddViewModel : ViewModelBase
    {
        private DateTime _showtime;
        private string _errorMessage;
        private ProjectorService showtimeManager;
        public ObservableCollection<string> ListId { get; set; }

        public DateTime Showtime { get => _showtime; set { _showtime = value; OnPropertyChanged(nameof(Showtime)); } }
        public string ErrorMessage { get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); } }
        public ICommand AddCommand { get; }
        public List<Projector> newShowtime { get; set; }
        public Window window { get; set; }

        public ShowtimeAddViewModel(Window currentWindow)
        {
            window = currentWindow;
            _errorMessage = "";
            AddCommand = new ViewModelCommand(ExecutedAddCommand);
            showtimeManager = new ProjectorService();
            newShowtime = new List<Projector>();
        }

        private void ExecutedAddCommand(object obj)
        {
            if (Showtime < DateTime.Now)
            {
                ErrorMessage = "Giá trị không hợp lệ!";
            }
            else
            {
                ErrorMessage = "";
                Projector temp = new Projector { ProjectorInfo = Showtime, Tickets = new List<Ticket>() };
                showtimeManager.addProjector(temp);
                newShowtime.Add(temp);
                window.DialogResult = true;
                window.Close();
            }
        }

    }
}
