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
            var service = DemoProxy.CustomerService();
            var result = await service.GetCustomer(id);

            return result;
        }

        /// <summary>
        /// Gets the customers.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            var service = DemoProxy.CustomerService();
            var result = await service.GetAllCustomers();

            return result;
        }

        /// <summary>
        /// Count of Customers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<int> CustomerCount()
        {
            var service = DemoProxy.CustomerService();
            var result = await service.CustomerCount();

            return result;
        }

        /// <summary>
        /// Count of Customers.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task AddCustomer(Customer customer)
        {
            var service = DemoProxy.CustomerService();
            await service.AddCustomer(customer);
        }

        /// <summary>
        /// Count of Customers.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task RemoveCustomer(int customerId)
        {
            var service = DemoProxy.CustomerService();
            await service.RemoveCustomer(customerId);
        }
    }
}
