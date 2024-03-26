using CineManagement.Models;
using CineManagement.ViewModels;
using System.Windows;

namespace CineManagement.Views
{
    public partial class UpdateShowtime : Window
    {
        public Projector updateShowtime { get; set; }

        public UpdateShowtime(Projector currentShowtime)
        {
            InitializeComponent();
            var context = new ShowtimeUpdateViewModel(currentShowtime, this);
            this.DataContext = context;
            context.showtime = currentShowtime;
        }
    }
}
