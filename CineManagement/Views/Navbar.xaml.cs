using System.Windows;
using CineManagement.ViewModels;
using UserControl = System.Windows.Controls.UserControl;

namespace CineManagement.Views
{
    public partial class Navbar : UserControl
    {
        public Navbar()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.DataContext = new LoginViewModel();
            Window window = Window.GetWindow(this);
            window.Close();
            login.Show();
        }

        private void Logo_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Window.GetWindow(this);
            mw.HomeView.DataContext = new HomeViewModel();
            mw.HideAllExcept(mw.Root, mw.HomeView);
        }

        private void BuyBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Window.GetWindow(this);
            mw.AllMoviesView.DataContext = new AllMoviesViewModel("");
            mw.HideAllExcept(mw.Root, mw.AllMoviesView);
        }

        private void UserBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Window.GetWindow(this);
            MainWindowViewModel vm = (MainWindowViewModel)Window.GetWindow(this).DataContext;

            mw.UserDetailsView.DataContext = new UserDetailViewModel(vm);
            mw.HideAllExcept(mw.Root, mw.UserDetailsView);
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Window.GetWindow(this);
            mw.AllMoviesView.DataContext = new AllMoviesViewModel(searchBox.Text);
            mw.HideAllExcept(mw.Root, mw.AllMoviesView);
        }
    }
}
