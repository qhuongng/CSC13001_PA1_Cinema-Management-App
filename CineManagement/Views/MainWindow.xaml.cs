using CineManagement.Models;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace CineManagement
{
    public partial class MainWindow : MetroWindow
    {
        bool IsFullScreen = false;
        WindowState OldWindowState;

        public User currentUser { get; set; }
 
        public MainWindow()
        {   
            InitializeComponent();
            DataContext = this;
        }

        public MainWindow(User user)
        {
            currentUser = user;

            InitializeComponent();
            DataContext = this;
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

        public void HideAllExcept(UIElement parent, UIElement visibleElement)
        {
            var childNumber = VisualTreeHelper.GetChildrenCount(parent);

            for (var i = 0; i < childNumber; i++)
            {
                var uiElement = VisualTreeHelper.GetChild(parent, i) as UIElement;

                if (uiElement != null && uiElement == visibleElement)
                {
                    visibleElement.Visibility = Visibility.Visible;
                }

                if (uiElement != null && uiElement != visibleElement)
                {
                    uiElement.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}