using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CineManagement.Views
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        bool IsFullScreen = false;
        WindowState OldWindowState;

        public Admin()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void closeWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Application.Current.Shutdown();
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
    }
}
