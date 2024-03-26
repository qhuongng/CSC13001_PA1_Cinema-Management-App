using CineManagement.Models;
using CineManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace CineManagement.ViewModels
{
    public class AdminDetailViewModel: ViewModelBase
    {
        public User user { get; set; }
        public Window window { get; set; }
        private UserService userManage;
        private string _userName;
        private string _password;
        private DateTime _dob;
        private string _errorMessage;
        private string _successMessage;

        public ICommand UpdateCommand { get; }
        public string UserName { get => _userName; set { _userName = value; OnPropertyChanged(nameof(UserName)); } }
        public string Password { get => _password; set { _password = value; OnPropertyChanged(nameof(Password)); } }
        public DateTime Dob { get => _dob; set { _dob = value; OnPropertyChanged(nameof(Dob)); } }
        public string ErrorMessage { get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); } }
        public string SuccessMessage { get => _successMessage; set { _successMessage = value; OnPropertyChanged(nameof(SuccessMessage)); } }

        public AdminDetailViewModel(User currentUser, Window currentWindow)
        {
            user = currentUser;
            window = currentWindow;
            userManage = new UserService();
            UpdateCommand = new ViewModelCommand(ExecuteUpdateCommand);
            _userName = user.UserName;
            _password = user.Password;
            _dob = user.Dob.ToDateTime(new TimeOnly(00, 00));

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
                    window.DialogResult = true;
                    window.Close();
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
