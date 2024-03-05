using System.Windows;

namespace CineManagement
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public bool registerFlag = false;
        public User userLogin;
        private UserManager userManager;
        public Login()
        {
            InitializeComponent();
        }
        public void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string userName = txtUsername.Text;
            string password = txtPassword.ToString();
            if(userName.Length > 0 && password.Length > 0)
            {
                try
                {
                    userLogin = userManager.GetUserByName(userName);
                    if (userLogin != null)
                    {
                        System.Windows.Forms.MessageBox.Show("Login successful!");
                        DialogResult = true;
                    }
                } catch(Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
                
            }
        }
        public void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            registerFlag = true;
            DialogResult = false;
            Close();
        }
    }
    
}
