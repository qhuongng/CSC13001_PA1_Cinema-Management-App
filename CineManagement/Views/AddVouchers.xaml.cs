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
    /// Interaction logic for AddVouchers.xaml
    /// </summary>
    public partial class AddVouchers : Window
    {
        public List<Voucher> addVoucher { get; set; }

        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        public AddVouchers()
        {
            InitializeComponent();
            VoucherAddViewModel viewModel = new VoucherAddViewModel(this);
            this.DataContext = viewModel;
            addVoucher = viewModel.newVoucher;
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
    }
}
