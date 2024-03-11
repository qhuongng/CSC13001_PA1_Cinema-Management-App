using MahApps.Metro.Controls;
using System.Collections.ObjectModel;

namespace CineManagement
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}