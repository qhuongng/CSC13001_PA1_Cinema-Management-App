using CineManagement.Models;
using CineManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineManagement.ViewModels
{
    class VoucherAddViewModel:ViewModelBase
    {
        private int _discountPercentage;
        private string _errorMessage;
        private string _selectedID;
        private VoucherService voucherManager;
        private UserService userManager;
        public ObservableCollection<string> ListId { get; set; }
        private List<User> UserList;
        

        public int DiscountPercentage { get => _discountPercentage; set { _discountPercentage = value; OnPropertyChanged(nameof(DiscountPercentage)); } }
        public string SelectedId { get => _selectedID; set { _selectedID = value; OnPropertyChanged(nameof(SelectedId)); } }
        public string ErrorMessage { get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); } }
        public ICommand AddCommand { get; }
        public List<Voucher> newVoucher { get; set; }

        public VoucherService voucherService;
        public Window window { get; set; }
        public VoucherAddViewModel(Window currentWindow)
        {
            window = currentWindow;
            newVoucher = new List<Voucher>();
            _errorMessage = "";
            AddCommand = new ViewModelCommand(ExecutedAddCommand);
            voucherManager = new VoucherService();
            userManager = new UserService();
            UserList = userManager.getAllUser();
            ListId = new ObservableCollection<string>();
            foreach(User user in UserList)
            {
                MessageBox.Show(user.UserId.ToString());
                ListId.Add(user.UserId.ToString());
            }
            ListId.Add("Tất cả");
        }

        private void ExecutedAddCommand(object obj)
        {
            if (DiscountPercentage >= 100)
            {
                ErrorMessage = "Giá trị không hợp lệ!";
            }
            else
            {
                ErrorMessage = "";
                if (SelectedId.CompareTo("Tất cả") == 0)
                {
                    foreach (User u in UserList)
                    {
                        if (u.IsAdmin == false)
                        {
                            Voucher temp = new Voucher{ DiscountPercent = _discountPercentage, UserId = u.UserId, IsUsed = false };
                            voucherManager.addVoucher(temp);
                            newVoucher.Add(temp);
                            window.DialogResult = true;
                            window.Close();
                        }
                    }
                }
                else
                {
                   int userID = int.Parse(SelectedId);
                   Voucher temp = new Voucher { DiscountPercent = _discountPercentage, UserId = userID, IsUsed = false };
                   voucherManager.addVoucher(temp);
                   newVoucher.Add(temp);
                   window.DialogResult = true;
                   window.Close();
                }
            }
        }

    }
}
