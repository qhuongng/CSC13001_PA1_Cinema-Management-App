using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace CineManagement
{
    public partial class Register : Window
    {
        public bool loginFlag = false;
        public User newUser;
        private UserManager userManager;
        public string Text { get; set; }
        public Register()
        {
            InitializeComponent();
            newUser = new User();
            userManager = new UserManager();
        }
        public void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            string userName = txtUsername.Text;
            string password = txtPassword.ToString();
            if(userName.Length > 0 && password.Length > 0 && dtpDOB.SelectedDate.HasValue)
            {
                DateTime dob = dtpDOB.SelectedDate.Value.Date;
                string gender = "Male";
                if (rbFemale.IsChecked == true)
                {
                    gender = "Female";
                }
                if (dob < DateTime.Now)
                {
                    newUser.userName = userName;
                    newUser.password = password;
                    newUser.gender = gender;
                    newUser.DOB = dob;
                    try
                    {
                        User addedUser = userManager.addUser(newUser);
                        if (addedUser != null)
                        {
                            System.Windows.Forms.MessageBox.Show("Register successful!");
                            DialogResult = true;
                            newUser = addedUser;
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                    }
                }
            } 
        }
        public void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            loginFlag = true;
            Close();
        }
    }
    public class formRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Debug.WriteLine("hello");
            string text = "";
            if (((string)value).Length > 0)
            {
                text = (string)value;
            }
            else
            {
                return new ValidationResult(false, "This field can not be empty");
            }
            return ValidationResult.ValidResult;
        }
    }
}
