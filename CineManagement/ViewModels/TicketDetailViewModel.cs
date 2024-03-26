using CineManagement.Models;

namespace CineManagement.ViewModels
{
    class TicketDetailViewModel : ViewModelBase
    {
        private Movie _movie;
        private Ticket _ticket;
        private string _selectedSeat;

        public Movie Movie { get => _movie; set { _movie = value; OnPropertyChanged(nameof(Movie)); } }
        public Ticket Ticket { get => _ticket; set { _ticket = value; OnPropertyChanged(nameof(Ticket)); } }
        public string SelectedSeat { get => _selectedSeat; set { _selectedSeat = value; OnPropertyChanged(nameof(SelectedSeat)); } }
        public List<string> Seats { get; set; }

        public TicketDetailViewModel(Ticket ticket)
        {
            if (ticket != null)
            {
                _ticket = ticket;
                _movie = ticket.Movie;
                _selectedSeat = ticket.SeatId.Trim();

                Seats = new List<string>();

                // populate seat chart
                char startChar = 'A';
                int maxRows = 8;
                int maxColumns = 10;

                for (int row = 1; row <= maxRows; row++)
                {
                    for (int column = 1; column <= maxColumns; column++)
                    {
                        char currentChar = (char)(startChar + row - 1);
                        Seats.Add($"{currentChar}{column}");
                    }
                }
            }
        }
    }
}
