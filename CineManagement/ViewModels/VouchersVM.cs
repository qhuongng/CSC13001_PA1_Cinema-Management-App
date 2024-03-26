using CineManagement.Models;
using CineManagement.Services;
using CineManagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineManagement.ViewModels
{
    class VouchersVM : ViewModelBase
    {
        private int _totalVoucher;
        private ObservableCollection<Voucher> _vouchers;
        private Voucher _selectedVoucher;
        public VoucherService voucherManager;

        public ICommand addCommand { get; }
        public ICommand updateCommand { get; }
        public ICommand deleteCommand { get; }
        public int TotalVoucher { get => _totalVoucher; set { _totalVoucher = value; OnPropertyChanged(nameof(TotalVoucher)); } }
        public ObservableCollection<Voucher> Vouchers { get => _vouchers; set { _vouchers = value; OnPropertyChanged(nameof(Vouchers)); } }
        public Voucher SelectedVoucher { get => _selectedVoucher; set { _selectedVoucher = value; OnPropertyChanged(nameof(SelectedVoucher)); } }

        public VouchersVM()
        {
            voucherManager = new VoucherService();
            _totalVoucher = 0;
            addCommand = new ViewModelCommand(executedAddCommand);
            updateCommand = new ViewModelCommand(executedUpdateCommand);
            deleteCommand = new ViewModelCommand(executedDeleteCommand);
            try
            {
                var vch = voucherManager.getVouchers();
                _vouchers = new ObservableCollection<Voucher>(vch);
                _totalVoucher = vch.Count;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void executedDeleteCommand(object obj)
        {
            if (_selectedVoucher != null)
            {
                try
                {
                    bool checkdel = voucherManager.deleteVoucherById(_selectedVoucher.VoucherId);
                    if (checkdel)
                    {
                        _vouchers.Remove(_selectedVoucher);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void executedUpdateCommand(object obj)
        {
            if (_selectedVoucher != null)
            {
                var UpdateScreen = new UpdateVouchers(_selectedVoucher);
                UpdateScreen.ShowDialog();
                if (UpdateScreen.DialogResult == true)
                {
                    _selectedVoucher = UpdateScreen.updateVoucher;
                }
            }
        }

        private void executedAddCommand(object obj)
        {
            var AddScreen = new AddVouchers();
            AddScreen.ShowDialog();
            if(AddScreen.DialogResult == true)
            {
                List<Voucher> temp = AddScreen.addVoucher;
                foreach (Voucher voucher in temp)
                {
                    _vouchers.Add(voucher);
                }
            }
           
        }
    }
}
