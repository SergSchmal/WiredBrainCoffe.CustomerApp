using System;
using System.Linq;
using Windows.ApplicationModel;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WiredBrainCoffee.CustomerApp.DataProvider;
using WiredBrainCoffee.CustomerApp.Model;
using WiredBrainCoffee.CustomerApp.ViewModel;

namespace WiredBrainCoffee.CustomerApp
{
    public sealed partial class MainPage
    {
        private readonly CustomerDataProvider _customerDataProvider;
        private MainViewModel _mainViewModel;

        public MainPage()
        {
            InitializeComponent();
            _mainViewModel = new MainViewModel(new CustomerDataProvider());
            DataContext = _mainViewModel;
            Loaded += OnLoaded;
            Application.Current.Suspending += App_Suspending;
            _customerDataProvider = new CustomerDataProvider();
            RequestedTheme = Application.Current.RequestedTheme == ApplicationTheme.Light ? ElementTheme.Dark : ElementTheme.Light;
        }

        private async void App_Suspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await _mainViewModel.SaveAsync();
            deferral.Complete();
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            await _mainViewModel.LoadAsync();
        }

        private void ButtonAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            var customer = new Customer {FirstName = "New"};
            customerListView.Items?.Add(customer);
            customerListView.SelectedItem = customer;
        }

        private void ButtonDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (customerListView.SelectedItem is Customer customer)
            {
                customerListView.Items?.Remove(customer);
            }
        }

        private void ButtonMove_Click(object sender, RoutedEventArgs e)
        {
            var column = Grid.GetColumn(customerListGrid);

            int newColumn = column == 0 ? 2 : 0;

            Grid.SetColumn(customerListGrid, newColumn);

            moveSymbolIcon.Symbol = newColumn == 0 ? Symbol.Forward : Symbol.Back;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            RequestedTheme = RequestedTheme == ElementTheme.Dark ? ElementTheme.Light : ElementTheme.Dark;
        }
    }
}
