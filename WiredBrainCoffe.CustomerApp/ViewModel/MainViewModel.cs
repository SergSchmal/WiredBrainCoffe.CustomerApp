using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WiredBrainCoffee.CustomerApp.DataProvider;
using WiredBrainCoffee.CustomerApp.Model;

namespace WiredBrainCoffee.CustomerApp.ViewModel
{
    public class MainViewModel
    {
        private readonly ICustomerDataProvider _customerDataProvider;

        public MainViewModel(ICustomerDataProvider customerDataProvider)
        {
            _customerDataProvider = customerDataProvider;
            Customers = new ObservableCollection<Customer>();
        }

        public ObservableCollection<Customer> Customers { get; }

        public async Task LoadAsync()
        {
            Customers.Clear();

            var customers = await _customerDataProvider.LoadCustomersAsync();
            foreach (var customer in customers) Customers.Add(customer);
        }

        public async Task SaveAsync()
        {
            await _customerDataProvider.SaveCustomersAsync(Customers);
        }
    }
}