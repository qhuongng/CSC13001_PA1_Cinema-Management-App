using CineManagement.Models;
using CineManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CineManagement.ViewModels
{
    public class TicketInfo
    {
        public byte[] MovieImage { get; set; }
        public string MovieName { get; set; }
        public DateTime ProjectorInfo { get; set; }
        public int Duration { get; set; }
        public string SeatId { get; set; }
        public int Price { get; set; }
        public TicketInfo(byte[] image, string name, DateTime showTime, int duration, string seat, int price) 
        {
            MovieImage = image;
            MovieName = name;
            ProjectorInfo = showTime;
            Duration = duration;
            SeatId = seat;
            Price = price;
        }
    }
    public class UserDetailViewModel : ViewModelBase
    {
        public User user, tempUser;
        private UserService userManage;
        private TicketService ticketManage;
        private ObservableCollection<TicketInfo> ticketInfoList;
        

        private string _userName;
        private string _password;
        private DateTime _dob;
        private string _errorMessage;
        private string _successMessage;
        private bool _isViewVisible = true;

        public ICommand UpdateCommand { get; }
        public string UserName { get => _userName; set { _userName = value;OnPropertyChanged(nameof(UserName)); } }
        public string Password { get => _password; set { _password = value;OnPropertyChanged(nameof(Password)); } }
        public DateTime Dob { get => _dob; set { _dob = value;OnPropertyChanged(nameof(Dob)); } }
        public string ErrorMessage { get => _errorMessage; set { _errorMessage = value;OnPropertyChanged(nameof(ErrorMessage)); } }
        public bool IsViewVisible { get => _isViewVisible; set { _isViewVisible = value; OnPropertyChanged(nameof(IsViewVisible)); } }

        public string SuccessMessage { get => _successMessage; set { _successMessage = value; OnPropertyChanged(nameof(SuccessMessage)); } }

        public ObservableCollection<TicketInfo> TicketInfoList { get => ticketInfoList; set => ticketInfoList = value; }

        public UserDetailViewModel(User currentUser)
        {
            user = currentUser;
            userManage = new UserService();
            ticketManage = new TicketService();
            ticketInfoList = new ObservableCollection<TicketInfo>();
            UpdateCommand = new ViewModelCommand(ExecuteUpdateCommand);
            _userName = currentUser.UserName;
            _password = currentUser.Password;
            _dob = currentUser.Dob.ToDateTime(new TimeOnly(00,00));
            // set up data for ticket
            ticketManage = new TicketService();

            foreach(Ticket ticket in currentUser.Tickets)
            {
                Ticket preticket = ticketManage.GetTicketInfo(ticket.TicketId);
                ticketInfoList.Add(new TicketInfo(preticket.Movie.Poster, preticket.Movie.MovieName, preticket.Projector.ProjectorInfo, preticket.Movie.Duration, preticket.SeatId, preticket.Price));
            }

        }

        private void ExecuteUpdateCommand(object obj)
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "* Please fill all blanks!";
                SuccessMessage = "";
            }
            else if (Dob >= DateTime.Today)
            {
                ErrorMessage = "* Value of birthday is unacceptable!";
                SuccessMessage = "";
            }
            else if (UserName.Equals(user.UserName) && Password.Equals(user.Password) && DateOnly.FromDateTime(Dob) == user.Dob)
            {
                ErrorMessage = "* Nothing to Update!";
                SuccessMessage = "";
            }
            else
            {
                try
                {
                    user.UserName = UserName;
                    user.Password = Password;
                    user.Dob = DateOnly.FromDateTime(Dob);
                    userManage.updateUser(user);
                    ErrorMessage = "";
                    SuccessMessage = "* Update successfully";
                }
                catch (Exception ex)
                {
                    ErrorMessage = "* " + ex.Message;
                    SuccessMessage = "";
                }
            }
        }
    }
}
