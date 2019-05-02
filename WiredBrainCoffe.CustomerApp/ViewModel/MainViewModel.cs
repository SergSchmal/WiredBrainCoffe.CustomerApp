using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WiredBrainCoffee.CustomerApp.Base;
using WiredBrainCoffee.CustomerApp.DataProvider;
using WiredBrainCoffee.CustomerApp.Model;

namespace WiredBrainCoffee.CustomerApp.ViewModel
{
    public class MainViewModel : Observable
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

        private Customer _selectedCustomer;

        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                if (_selectedCustomer == value) return;
                _selectedCustomer = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsCustomerSelected));
            }
        }

        public bool IsCustomerSelected => SelectedCustomer != null;

        public void AddCustomer()
        {
            var customer = new Customer { FirstName = "New" };
            Customers.Add(customer);
            SelectedCustomer = customer;
        }

        public void DeleteCustomer()
        {
            var customer = SelectedCustomer;
            if (customer != null)
            {
                Customers.Remove(customer);
                SelectedCustomer = null;
            }
        }
    }
}