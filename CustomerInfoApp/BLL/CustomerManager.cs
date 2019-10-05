using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerInfoApp.Model;
using CustomerInfoApp.Repository;

namespace CustomerInfoApp.BLL
{
    public class CustomerManager
    {
        CustomerRepository _customerRepository = new CustomerRepository();

        public bool Add(Customer customer)
        {
            return _customerRepository.Add(customer);
        }

        public bool CheckIfNumeric(string input)
        {
            return _customerRepository.CheckIfNumeric(input);
        }

        public bool IsNameExist(Customer customerName)
        {
            return _customerRepository.IsNameExist(customerName);
        }

        public bool IsContactExist(Customer contact)
        {
            return _customerRepository.IsContactExist(contact);
        }

        public DataTable Display()
        {
            return _customerRepository.Display();
        }

        public bool Update(Customer customer, int id)
        {
            return _customerRepository.Update(customer,id);
        }

        public DataTable Search(Customer customer)
        {
            return _customerRepository.Search(customer);
        }
    }
}
