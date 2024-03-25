using CineManagement.Models;
using CineManagement.Services;
using CineManagement.Views;
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
    public class VoucherList
    {
        public int Number { get; set; }
        public Voucher Vouchers { get; set; }

        public VoucherList(int num, Voucher vch)
        {
            Number = num;
            Vouchers = vch;
        }
    }
    class VouchersVM:ViewModelBase
    {
        private int _totalVoucher;
        private ObservableCollection<VoucherList> _vouchers;
        private VoucherList _selectedVoucher;
        public VoucherService vouchersSV;


        public ICommand addCommand { get; }
        public ICommand updateCommand { get; }
        public ICommand deleteCommand { get; }
        public int TotalVoucher { get => _totalVoucher; set { _totalVoucher = value; OnPropertyChanged(nameof(TotalVoucher)); } }
        public ObservableCollection<VoucherList> Vouchers { get => _vouchers; set { _vouchers = value; OnPropertyChanged(nameof(Voucher)); } }
        public VoucherList SelectedVoucher { get => _selectedVoucher; set { _selectedVoucher = value; OnPropertyChanged(nameof(SelectedVoucher)); } }

        public VouchersVM()
        {
            vouchersSV = new VoucherService();
            _totalVoucher = 0;
            addCommand = new ViewModelCommand(executedAddCommand);
            updateCommand = new ViewModelCommand(executedUpdateCommand);
            deleteCommand = new ViewModelCommand(executedDeleteCommand);
            try
            {
                var vch = vouchersSV.getVouchers();
                _vouchers = new ObservableCollection<VoucherList>();
                _totalVoucher += vch.Count;
                int count = 1;
                
                foreach (Voucher x in vch)
                {
                    _vouchers.Add(new VoucherList(count,x));
                    count++;
                }


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
                    bool checkdel = vouchersSV.deleteVoucherById(_selectedVoucher.Vouchers.VoucherId);
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
                var UpdateScreen = new UpdateVouchers(_selectedVoucher.Vouchers);
                UpdateScreen.ShowDialog();
                if (UpdateScreen.DialogResult == true)
                {
                    _selectedVoucher.Vouchers = UpdateScreen.updateVoucher;
                }
            }
        }

        private void executedAddCommand(object obj)
        {
            var AddScreen = new AddVouchers();
            AddScreen.ShowDialog();
        }
    }
}
