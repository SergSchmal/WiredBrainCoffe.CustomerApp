using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WiredBrainCoffee.CustomerApp.Model;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace WiredBrainCoffee.CustomerApp.Controls
{
    public sealed partial class CustomerDetailControl : UserControl
    {
        public CustomerDetailControl()
        {
            this.InitializeComponent();
        }

        private Customer _customer;

        public Customer Customer
        {
            get => _customer;
            set
            {
                _customer = value;
                txtFirstName.Text = _customer.FirstName;
                txtLastName.Text = _customer.LastName;
                chkIsDeveloper.IsChecked = _customer.IsDeveloper;
            }
        }


        private void UpdateCustomer()
        {
            Customer.FirstName = txtFirstName.Text;
            Customer.LastName = txtLastName.Text;
            if (chkIsDeveloper.IsChecked != null) Customer.IsDeveloper = (bool)chkIsDeveloper.IsChecked;
        }

        private void TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCustomer();
        }

        private void ChkIsDeveloper_OnChecked(object sender, RoutedEventArgs e)
        {
            UpdateCustomer();
        }
    }
}
