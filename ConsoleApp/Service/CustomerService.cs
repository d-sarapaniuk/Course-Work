using HotelReservationService.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationService.Service
{
    public class CustomerService : Service
    {
        public enum SortingOptions
        {
            ById,
            ByLastName,
            ByFirstName
        }
        public void AddCustomer(string firstName, string lastName, string phone)
        {
            Customer customer = new Customer()
            {
                FirstName = firstName,
                LastName = lastName,
                Phone = phone
            };
            Add(customer);
        }
        public void RemoveCustomer(int customerId)
        {
            Customer? customer = context.Customers.Where(c => c.Id == customerId).FirstOrDefault();
            Remove(customer);
        }
        public void UpdateCustomer(int customerId, string firstName, string lastName, string phone)
        {
            var customer = context.Customers.Where(c => c.Id == customerId).FirstOrDefault();
            if (customer != null)
            {
                customer.FirstName = firstName;
                customer.LastName = lastName;
                customer.Phone = phone;
                context.SaveChanges();
            }
        }
        public Dictionary<int, string> getCustomers(SortingOptions sortingOption = SortingOptions.ById)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            IOrderedQueryable<Customer> customers;
            switch (sortingOption)
            {
                case SortingOptions.ByLastName:
                    customers = context.Customers.OrderBy(c => c.LastName);
                    break;
                case SortingOptions.ByFirstName:
                    customers = context.Customers.OrderBy(c => c.FirstName);
                    break;
                default:
                    customers = context.Customers.OrderBy(c => c.Id);
                    break;
            }
            foreach (Customer c in customers)
            {
                dict.Add(c.Id, c.FirstName + ' ' + c.LastName + ", " + c.Phone);
            }
            return dict;
        }
        public List<string> Search(string keyword)
        {
            var customers = context.Customers
                .OrderBy(c => c.LastName)
                .Where(c => c.FirstName.Contains(keyword) == true || c.LastName.Contains(keyword) == true).ToList();
            List<string> list = new List<string>();
            foreach(Customer c in customers)
            {
                list.Add(c.LastName + ' ' + c.FirstName);
            }
            return list;
        }


    }
}
