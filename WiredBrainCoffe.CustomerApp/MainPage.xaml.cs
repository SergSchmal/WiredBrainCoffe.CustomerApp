using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WiredBrainCoffee.CustomerApp.DataProvider;
using WiredBrainCoffee.CustomerApp.ViewModel;

namespace WiredBrainCoffee.CustomerApp
{
    public sealed partial class MainPage
    {
        private readonly MainViewModel _mainViewModel;

        public MainPage()
        {
            InitializeComponent();
            _mainViewModel = new MainViewModel(new CustomerDataProvider());
            Loaded += OnLoaded;
            Application.Current.Suspending += App_Suspending;
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
