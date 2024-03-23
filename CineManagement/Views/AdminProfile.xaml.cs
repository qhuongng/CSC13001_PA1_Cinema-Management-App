using CineManagement.Models;
using CineManagement.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CineManagement.Views
{
    
    public partial class AdminProfile : Window
    {
        public User user;
        public AdminProfile(User currentUser)
        {
            
            InitializeComponent();
            user = currentUser;
            DataContext = new AdminDetailViewModel(currentUser, this);
        }
    }
}
