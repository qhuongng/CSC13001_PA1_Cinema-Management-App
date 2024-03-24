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
        public ICommand TransactionsCommand { get; set; }
        public ICommand ShipmentsCommand { get; set; }
        public ICommand SettingsCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM(User);
        private void Customer(object obj) => CurrentView = new CustomerVM();
        private void Movies(object obj) => CurrentView = new MovieControlViewModel();
        private void Order(object obj) => CurrentView = new OrderVM();
        private void Transaction(object obj) => CurrentView = new TransactionVM();
        private void Shipment(object obj) => CurrentView = new ServiceVM();
        private void Setting(object obj) => CurrentView = new SettingVM();



        public NavigationVM(User user)
        {
            User = user;
            HomeCommand = new RelayCommand(Home);
            CustomersCommand = new RelayCommand(Customer);
            MoviesCommand = new RelayCommand(Movies);
            OrdersCommand = new RelayCommand(Order);
            TransactionsCommand = new RelayCommand(Transaction);
            ShipmentsCommand = new RelayCommand(Shipment);
            SettingsCommand = new RelayCommand(Setting);

            // Startup Page
            CurrentView = new HomeVM(user);
        }
    }
}

