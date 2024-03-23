using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CineManagement.Views;

namespace CineManagement
{
    public partial class MainWindow : MetroWindow
    {
        bool IsFullScreen = false;
        WindowState OldWindowState;
 
        public MainWindow()
        {   
            InitializeComponent();
        }

        private void closeWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void fullSrcBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFullScreen)
            {
                WindowBorder.Margin = new Thickness(0);
                WindowBorder.CornerRadius = new CornerRadius(0);
                WindowHeader.CornerRadius = new CornerRadius(0);
                Footer.FooterBorder.CornerRadius = new CornerRadius(0);

                OldWindowState = WindowState;
                WindowState = WindowState.Maximized;
                Visibility = Visibility.Collapsed;
                ResizeMode = ResizeMode.NoResize;
                Visibility = Visibility.Visible;
                Activate();
            }
            else
            {
                WindowBorder.Margin = new Thickness(5);
                WindowBorder.CornerRadius = new CornerRadius(10);
                WindowHeader.CornerRadius = new CornerRadius(10,10,0,0);
                Footer.FooterBorder.CornerRadius = new CornerRadius(0,0,0,10);

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