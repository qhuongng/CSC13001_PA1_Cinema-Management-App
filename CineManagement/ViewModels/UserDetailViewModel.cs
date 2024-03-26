using CineManagement.Models;
using CineManagement.Services;
using System.Windows.Input;

namespace CineManagement.ViewModels
{
    public class UserDetailViewModel : ViewModelBase
    {
        public User user { get; set; }
        private UserService userManage;
        private TicketService ticketManage;
        private List<Ticket> ticketInfoList;
        public MainWindowViewModel vm { get; set; }

        private string _userName;
        private string _password;
        private DateTime _dob;
        private string _errorMessage;
        private string _successMessage;
        private bool _isViewVisible = true;

        public ICommand UpdateCommand { get; }
        public string UserName { get => _userName; set { _userName = value;OnPropertyChanged(nameof(UserName)); } }
        public string Password { get => _password; set { _password = value;OnPropertyChanged(nameof(Password)); } }
        public DateTime Dob { get => _dob; set { _dob = value; OnPropertyChanged(nameof(Dob)); } }
        public string ErrorMessage { get => _errorMessage; set { _errorMessage = value;OnPropertyChanged(nameof(ErrorMessage)); } }
        public bool IsViewVisible { get => _isViewVisible; set { _isViewVisible = value; OnPropertyChanged(nameof(IsViewVisible)); } }
        public string SuccessMessage { get => _successMessage; set { _successMessage = value; OnPropertyChanged(nameof(SuccessMessage)); } }
        public List<Ticket> TicketInfoList { get => ticketInfoList; set { ticketInfoList = value; OnPropertyChanged(nameof(TicketInfoList)); } }

        public UserDetailViewModel(MainWindowViewModel viewModel)
        {
            vm = viewModel;
            user = vm.CurrentUser;
            userManage = new UserService();
            ticketManage = new TicketService();
            ticketInfoList = new List<Ticket>();
            UpdateCommand = new ViewModelCommand(ExecuteUpdateCommand);
            _userName = user.UserName;
            _password = user.Password;
            _dob = user.Dob.ToDateTime(new TimeOnly(00,00));
            // set up data for ticket
            ticketManage = new TicketService();
            user.Tickets = ticketManage.GetTicketsByUserId(user.UserId);
            TicketInfoList = user.Tickets.ToList();
        }

        private void ExecuteUpdateCommand(object obj)
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Vui lòng điền đầy đủ thông tin.";
                SuccessMessage = "";
            }
            else if (Dob >= DateTime.Today)
            {
                ErrorMessage = "Ngày sinh không hợp lệ.";
                SuccessMessage = "";
            }
            else if (UserName.Equals(user.UserName) && Password.Equals(user.Password) && DateOnly.FromDateTime(Dob) == user.Dob)
            {
                ErrorMessage = "Thông tin cá nhân không có thay đổi mới.";
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
                    vm.CurrentUser = user;
                    ErrorMessage = "";
                    SuccessMessage = "Cập nhật thông tin cá nhân thành công.";
                }
                catch (Exception ex)
                {
                    ErrorMessage = "" + ex.Message;
                    SuccessMessage = "";
                }
            }
        }
    }
}
