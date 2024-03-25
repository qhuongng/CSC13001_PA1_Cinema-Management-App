using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineManagement.Utilities;
using System.Windows.Input;
using CineManagement.ViewModels;
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

        private void OnPropertyChanged()
        {
            throw new NotImplementedException();
        }

        public ICommand HomeCommand { get; set; }
        public ICommand CustomersCommand { get; set; }
        public ICommand MoviesCommand { get; set; }
        public ICommand OrdersCommand { get; set; }
        public ICommand VouchersCommand { get; set; }
        public ICommand ServicesCommand { get; set; }
        public ICommand SettingsCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM(User);
        private void Customer(object obj) => CurrentView = new CustomerVM();
        private void Movies(object obj) => CurrentView = new MovieControlViewModel();
        private void Showtime(object obj) => CurrentView = new ShowtimeVM();
        private void Product(object obj) => CurrentView = new ProductVM();
        private void Order(object obj) => CurrentView = new OrderVM();
        private void Voucher(object obj) => CurrentView = new VouchersVM();
        private void Service(object obj) => CurrentView = new ServiceVM();
        private void Setting(object obj) => CurrentView = new SettingVM();



        public NavigationVM(User user)
        {
            User = user;
            HomeCommand = new RelayCommand(Home);
            CustomersCommand = new RelayCommand(Customer);
            MoviesCommand = new RelayCommand(Movies);
            ShowtimesCommand = new RelayCommand(Showtime);
            ProductsCommand = new RelayCommand(Product);
            OrdersCommand = new RelayCommand(Order);
            VouchersCommand = new RelayCommand(Voucher);
            ServicesCommand = new RelayCommand(Service);
            SettingsCommand = new RelayCommand(Setting);

            // Startup Page
            CurrentView = new HomeVM(user);
        }
    }
}

