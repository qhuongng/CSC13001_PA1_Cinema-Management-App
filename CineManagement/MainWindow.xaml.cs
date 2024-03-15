using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Input;

namespace CineManagement
{
    public partial class MainWindow : MetroWindow
    {
        Boolean IsFullScreen = false;
        WindowState OldWindowState;
 
        public MainWindow()
        {   
            InitializeComponent();
            var login = new Login();
            login.Show();
            if(login.IsVisible == false && login.IsLoaded)
            {
                login.Close();
                System.Windows.MessageBox.Show(login.txtUserLogin.Text);
            }
        }

        private void HomeViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.HomeViewModel homeViewModelObject = new ViewModel.HomeViewModel();
            homeViewModelObject.LoadAllMovies();
            homeViewModelObject.LoadBannerPosters();

            homeViewControl.DataContext = homeViewModelObject;
        }

        private void closeWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void fullSrcBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFullScreen)
            {
                OldWindowState = WindowState;
                WindowState = WindowState.Maximized;
                Visibility = Visibility.Collapsed;
                ResizeMode = ResizeMode.NoResize;
                Visibility = Visibility.Visible;
                Activate();
            }
            else
            {
                WindowState = OldWindowState;
                ResizeMode = ResizeMode.CanResize;
            }

            IsFullScreen = !IsFullScreen;
        }

        private void minimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void HandleWindowDrag(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}