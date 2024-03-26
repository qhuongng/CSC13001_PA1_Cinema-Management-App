using CineManagement.Models;
using CineManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CineManagement.Views
{
    /// <summary>
    /// Interaction logic for UserDetails.xaml
    /// </summary>
    public partial class UserDetails : UserControl
    {
        public UserDetails()
        {
            InitializeComponent();
        }

        private void TicketHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView lv = sender as ListView;
            Ticket ticket = lv.SelectedItem as Ticket;

            TicketDetail td = new TicketDetail();
            td.DataContext = new TicketDetailViewModel(ticket);
            td.ShowDialog();
        }
    }
}
