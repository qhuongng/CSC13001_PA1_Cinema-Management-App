using System.IO;
using System.Windows;
using System.Drawing;
using System.Windows.Forms;
using CineManagement.Services;
using System.Reflection;
using CineManagement.Models;


namespace CineManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
    }
}