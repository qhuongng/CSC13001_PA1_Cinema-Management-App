using CineManagement.Models;
using CineManagement.Services;
using System.Windows;
using System.Windows.Input;

namespace CineManagement.ViewModels
{
    class MovieDetailsViewModel : ViewModelBase
    {
        ProjectorService ps = new ProjectorService();
        VoucherService vs = new VoucherService();
        TicketService ts = new TicketService();

        public bool PurchaseSuccessful { get; set; }
        public List<Projector> Projectors { get; set; }
        public List<Voucher> Vouchers { get; set; }
        public List<Seat> Seats { get; set; }

        public Projector SelectedProjector
        {
            get => _selectedProjector;
            set
            {
                _selectedProjector = value;

                if (SelectedProjector != null)
                {
                    List<Ticket> boughtTickets = ts.GetTicketsByMovieAndShowtime(SelectedMovie.MovieId, SelectedProjector.ProjectorId);
                    BoughtSeats = boughtTickets.Select(ticket => ticket.SeatId.Trim()).ToList();
                }
                else
                {
                    BoughtSeats.Clear();
                }

                OnPropertyChanged(nameof(SelectedProjector));
            }
        }

        public List<Seat> SelectedSeats
        {
            get => _selectedSeats;
            set
            {
                _selectedSeats = value;

                Total = 0;

                if (SelectedSeats != null)
                {
                    Total = _selectedSeats.Count * 90000;

                    if (SelectedVoucher != null)
                    {
                        Total = Total * (1.0 - ((double)_selectedVoucher.DiscountPercent / 100.0));
                    }
                }

                OnPropertyChanged(nameof(SelectedSeats));
            }
        }

        public Voucher SelectedVoucher
        {
            get => _selectedVoucher;
            set
            {
                _selectedVoucher = value;

                if (SelectedVoucher != null)
                {
                    Total = Total * (1.0 - ((double)_selectedVoucher.DiscountPercent / 100.0));
                }

                OnPropertyChanged(nameof(SelectedVoucher));
            }
        }

        public Movie SelectedMovie { get => _selectedMovie; set { _selectedMovie = value; OnPropertyChanged(nameof(SelectedMovie)); } }
        public User User { get => _user; set { _user = value; OnPropertyChanged(nameof(User)); } }
        public List<string> BoughtSeats { get => _boughtSeats; set { _boughtSeats = value; OnPropertyChanged(nameof(BoughtSeats)); } }
        public double Total { get => _total; set { _total = value; OnPropertyChanged(nameof(Total)); } }

        private Movie _selectedMovie;
        private Projector _selectedProjector;
        private List<Seat> _selectedSeats;
        private List<string> _boughtSeats;
        private Voucher _selectedVoucher;
        private double _total;
        private User? _user;
        private string _errorMsg;

        public ICommand PurchaseCommand { get; }
        public string ErrorMsg { get => _errorMsg; set { _errorMsg = value; OnPropertyChanged(nameof(ErrorMsg)); } }

        public MovieDetailsViewModel(Movie selected, User currentUser)
        {
            if (currentUser == null)
            {
                _user = null;
            }
            else
            {
                _user = currentUser;
            }

            PurchaseSuccessful = false;
                 
            _selectedMovie = selected;
            _total = 0;

            _selectedSeats = new List<Seat>();

            Projectors = new List<Projector>();
            Vouchers = new List<Voucher>();
            Seats = new List<Seat>();

            List<Projector> projectors = ps.getProjectors();
            Projectors = projectors;

            List<Voucher> vouchers = new List<Voucher>();

            if (_user != null)
            {
                vouchers = vs.GetUnusedVouchersByUserId(User.UserId);
            }
            
            Vouchers = vouchers;

            // populate seat chart
            char startChar = 'A';
            int maxRows = 8;
            int maxColumns = 10;

            for (int row = 1; row <= maxRows; row++)
            {
                for (int column = 1; column <= maxColumns; column++)
                {
                    char currentChar = (char)(startChar + row - 1);
                    Seats.Add(new Seat($"{currentChar}{column}"));
                }
            }

            PurchaseCommand = new ViewModelCommand(ExecutePurchaseCommand, CanExecutePurchaseCommand);
        }

        private bool CanExecutePurchaseCommand(object obj)
        {
            bool validData;

            if (User == null)
            {
                ErrorMsg = "Vui lòng đăng nhập/đăng kí tài khoản để đặt vé.";
                validData = false;
            }
            else if (SelectedProjector == null)
            {
                ErrorMsg = "Vui lòng chọn suất chiếu.";
                validData = false;
            }
            else if (SelectedSeats.Count == 0)
            {
                ErrorMsg = "Vui lòng chọn ít nhất một chỗ ngồi.";
                validData = false;
            }
            else
            {
                validData = true;
                ErrorMsg = "";
            }

            return validData;
        }

        private void ExecutePurchaseCommand(object obj)
        {;
            double indiPrice = (double)Total / SelectedSeats.Count;
            int purchaseCount = 0;

            foreach (Seat seat in SelectedSeats)
            {
                Ticket ticket = new Ticket() { UserId = User.UserId, MovieId=SelectedMovie.MovieId, SeatId=seat.SeatId, Price=indiPrice, ProjectorId=SelectedProjector.ProjectorId };

                try
                {
                    bool checkPurchase = ts.AddTicket(ticket);

                    if (checkPurchase == true)
                    {
                        BoughtSeats.Add(seat.SeatId.Trim());
                        purchaseCount++;
                    }
                }
                catch (Exception ex)
                {
                    ErrorMsg = ex.Message;
                }
            }

            if (purchaseCount == SelectedSeats.Count)
            {
                if (SelectedVoucher != null)
                {
                    vs.UpdateVoucherStatus(SelectedVoucher);
                    SelectedVoucher = null;
                }
                
                SelectedProjector = null;
                SelectedSeats.Clear();

                PurchaseSuccessful = true;
                MessageBox.Show("Đặt vé thành công.");
            }
        }
    }
}
