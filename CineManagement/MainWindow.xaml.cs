using MahApps.Metro.Controls;
using System.Collections.ObjectModel;
using System.Windows;

namespace CineManagement
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HomeViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Home homeViewModelObject = new ViewModel.Home();
            homeViewModelObject.LoadAllMovies();
            homeViewModelObject.LoadBannerPosters();

            homeViewControl.DataContext = homeViewModelObject;
        }   
    }
}