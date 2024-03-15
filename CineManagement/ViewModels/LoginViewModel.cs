using CineManagement.Models;
using CineManagement.Services;
using Prism.Commands;
using System.ComponentModel;
using System.Security;
using System.Windows.Forms;
using System.Windows.Input;

namespace CineManagement.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public User user;
        private UserService userManager;
        private string _usernameLogin;
        private string _passwordLogin;
        private string _usernameRegister;
        private string _passwordRegister;
        private DateTime _dob = DateTime.Now;
        private string _errorMessage;
        private string _errorMessageLogin;
        private string _errorMessageRegister;
        private string _errorMessageField;
        private bool _isViewVisible = true;

        public string UsernameLogin { get => _usernameLogin; set { _usernameLogin = value; OnPropertyChanged(nameof(UsernameLogin));} }
        public string PasswordLogin { get => _passwordLogin; set { _passwordLogin = value; OnPropertyChanged(nameof(PasswordLogin)); } }
        public string UsernameRegister { get => _usernameRegister; set { _usernameRegister = value; OnPropertyChanged(nameof(UsernameRegister)); } }
        public string PasswordRegister { get => _passwordRegister; set { _passwordRegister = value; OnPropertyChanged(nameof(PasswordRegister)); } }
        public DateTime Dob { get => _dob; set { _dob = value; OnPropertyChanged(nameof(Dob)); } }
        public string ErrorMessage { get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); } }
        public bool IsViewVisible { get => _isViewVisible; set { _isViewVisible = value; OnPropertyChanged(nameof(IsViewVisible)); } }
       
        // command
        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand LoginAsGuestCommand { get; }
        public string ErrorMessageRegister { get => _errorMessageRegister; set { _errorMessageRegister = value; OnPropertyChanged(nameof(ErrorMessageRegister)); } }

        public string ErrorMessageLogin { get => _errorMessageLogin; set { _errorMessageLogin = value; OnPropertyChanged(nameof(ErrorMessageLogin)); } }

        public string ErrorMessageField { get => _errorMessageField; set { _errorMessageField = value; OnPropertyChanged(nameof(ErrorMessageField)); } }

        public LoginViewModel()
        {
            userManager = new UserService();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RegisterCommand = new ViewModelCommand(ExecuteRegisterCommand, CanExecuteRegisterCommand);
            LoginAsGuestCommand = new ViewModelCommand(ExecuteGuestLoginCommand);
        }

        private void ExecuteGuestLoginCommand(object obj)
        {
            var mainScreen = new MainWindow();
            mainScreen.Show();
            IsViewVisible = false;
        }

        private bool CanExecuteRegisterCommand(object obj)
        {
            bool validData;
            DateTime temp;
            if(string.IsNullOrWhiteSpace(UsernameRegister) || string.IsNullOrWhiteSpace(PasswordRegister))
            {
                ErrorMessageRegister = "";
                ErrorMessageField = "* Please fill all blanks!";
                validData = false;
            } else if(Dob > new DateTime(2024,03,13))
            {
                ErrorMessageRegister = "";
                ErrorMessageField = "* Value of birthday is unacceptable!";
                validData = false;
            }
            else {
                validData= true;
                ErrorMessageField = "";
            }
            return validData;
        }

        private void ExecuteRegisterCommand(object obj)
        {
            user = new User() { UserName = UsernameRegister, Password = PasswordRegister, Dob = DateOnly.FromDateTime(Dob), IsAdmin = false };
            try
            {
                bool checkSignUp = userManager.addUser(user);
                if(checkSignUp == true)
                {
                    var mainScreen = new MainWindow(user);
                    mainScreen.Show();
                    IsViewVisible = false;
                }
            } catch (Exception ex)
            {
                ErrorMessageRegister = "* "+ ex.Message;
            }

        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(UsernameLogin) || string.IsNullOrWhiteSpace(PasswordLogin))
            {
                validData = false;
                ErrorMessageLogin = "";
                ErrorMessage = "* Please fill all blanks!";
            }
            else { 
                validData = true;
                ErrorMessage = "";
            }
            return validData;
        }

        private void ExecuteLoginCommand(object obj)
        {
            try
            {
                user = userManager.CheckLogin(UsernameLogin,PasswordLogin);
                if (user != null)
                {
                    var mainScreen = new MainWindow(user);
                    mainScreen.Show();
                    IsViewVisible=false;
                }
            } catch (Exception ex)
            {
                
                ErrorMessageLogin = "* " + ex.Message;
            }
        }
    }
}
