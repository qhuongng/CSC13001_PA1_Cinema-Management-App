using CineManagement.ViewModels;
using System.Windows.Controls;

namespace CineManagement.Views
{
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
            DataContext = new HomeViewModel();
        }
    }
}
