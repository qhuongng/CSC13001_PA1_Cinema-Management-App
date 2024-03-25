using CineManagement.Models;
using CineManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CineManagement.Views
{
    /// <summary>
    /// Interaction logic for UpdateVouchers.xaml
    /// </summary>
    public partial class UpdateVouchers : Window
    {
        public Voucher updateVoucher { get; set; }

        public UpdateVouchers(Voucher currentVoucher)
        {
            InitializeComponent();
            var context = new VoucherUpdateViewModel(currentVoucher, this);
            this.DataContext = context;
            updateVoucher = context.voucher;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
