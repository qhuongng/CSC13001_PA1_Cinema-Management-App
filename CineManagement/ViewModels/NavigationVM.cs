using CineManagement.Utilities;
using System.Windows.Input;
using CineManagement.Models;

namespace CineManagement.ViewModels
{
    class NavigationVM : ViewModelBase
    {
        private object _currentView;
        public User User { get; set; }
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(nameof(CurrentView)); }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand MoviesCommand { get; set; }
        public ICommand VouchersCommand { get; set; }
        public ICommand ShowtimesCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM(User);
        private void Movies(object obj) => CurrentView = new MovieControlViewModel();
        private void Voucher(object obj) => CurrentView = new VouchersVM();
        private void Showtimes(object obj) => CurrentView = new ShowtimeVM();

        public NavigationVM(User user)
        {
            User = user;
            HomeCommand = new RelayCommand(Home);
            MoviesCommand = new RelayCommand(Movies);
            VouchersCommand = new RelayCommand(Voucher);
            ShowtimesCommand = new RelayCommand(Showtimes);

            // Startup Page
            CurrentView = new HomeVM(user);
        }
    }
}

