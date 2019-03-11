using System;

namespace WiredBrainCoffee.CustomerApp.Model
{
    public class CustomerConverter
    {
        public static Customer CreateCustomerFromString(string input)
        {
            var values = input.Split(",");
            return new Customer
            {
                FirstName = values[0],
                LastName = values[1],
                IsDeveloper = Convert.ToBoolean(values[2])
            };
        }
    }
}