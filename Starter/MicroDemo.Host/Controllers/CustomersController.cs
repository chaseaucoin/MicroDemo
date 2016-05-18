using Demo.Contracts;
using Demo.Contracts.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroDemo.Host.Controllers
{
    public class CustomersController : ApiController
    {
        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Customer> GetCustomer(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the customers.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Count of Customers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<int> CustomerCount()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Count of Customers.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task AddCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Count of Customers.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task RemoveCustomer(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
