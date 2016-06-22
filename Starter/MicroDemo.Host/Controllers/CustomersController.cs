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
            return await Task.FromResult(new Customer() { Id = 0, FirstName = "Not", LastName = "Implemented" });
        }

        /// <summary>
        /// Gets the customers.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            var resultList = new List<Customer>();
            resultList.Add(new Customer() { Id = 0, FirstName = "Not", LastName = "Implemented" });
            return await Task.FromResult(resultList);
        }

        /// <summary>
        /// Count of Customers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<int> CustomerCount()
        {
            return await Task.FromResult(0);
        }

        /// <summary>
        /// Count of Customers.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task AddCustomer(Customer customer)
        {
            await Task.FromResult(0);
        }

        /// <summary>
        /// Count of Customers.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task RemoveCustomer(int customerId)
        {
            await Task.FromResult(0);
        }
    }
}
