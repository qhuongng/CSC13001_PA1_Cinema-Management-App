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
    class VoucherUpdateViewModel:ViewModelBase
    {
        private int _discountPercentage;
        private string _errorMessage;
        private string _successMessage;

        public int DiscountPercentage { get => _discountPercentage; set { _discountPercentage = value; OnPropertyChanged(nameof(DiscountPercentage)); } }
        public string ErrorMessage { get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); } }
        public string SuccessMessage { get => _successMessage; set { _successMessage = value; OnPropertyChanged(nameof(SuccessMessage)); } }

        public ICommand UpdateCommand { get; }

        public Voucher voucher;
        public VoucherService voucherService;
        public Window window;

        public VoucherUpdateViewModel(Voucher currentVoucher, Window currentWindow)
        {
            voucher = currentVoucher;
            window = currentWindow;
            _discountPercentage = voucher.DiscountPercent;
            _errorMessage = "";
            _successMessage = "";

            UpdateCommand = new ViewModelCommand(ExecutedUpdateCommand);
        }

        private void ExecutedUpdateCommand(object obj)
        {
            if (!int.TryParse(_discountPercentage.ToString(), out int discountPercentage))
            {
                ErrorMessage = "* Giá trị không hợp lệ!";
                SuccessMessage = "";
            }
            else if (_discountPercentage == voucher.DiscountPercent)
            {
                ErrorMessage = "* Nothing to update!";
                SuccessMessage = "";
            }
            else
            {
                voucher.DiscountPercent = _discountPercentage;
                try
                {
                    bool checkUpdate = voucherService.updateVoucher(voucher);
                    if (checkUpdate)
                    {
                        SuccessMessage = "Update successfully";
                        window.DialogResult = true;
                        window.Close();
                    }
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
