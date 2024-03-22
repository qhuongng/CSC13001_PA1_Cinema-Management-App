using CineManagement.Models;

namespace CineManagement.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private User _currentUser;

        public User CurrentUser { get => _currentUser; set { _currentUser = value; OnPropertyChanged(nameof(CurrentUser)); } }

        public MainWindowViewModel() {}

        public MainWindowViewModel(User currentUser)
        {
            _currentUser = currentUser;
        }
    }
}
