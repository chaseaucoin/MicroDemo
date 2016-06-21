using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Contracts.Customers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.ServiceFabric.Services.Remoting.IService" />
    public interface ICustomerService : Microsoft.ServiceFabric.Services.Remoting.IService
    {
        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<Customer> GetCustomer(int id);

        /// <summary>
        /// Gets the customers.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Customer>> GetAllCustomers();

        /// <summary>
        /// Count of Customers.
        /// </summary>
        /// <returns></returns>
        Task<int> CustomerCount();

        /// <summary>
        /// Adds the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <returns></returns>
        Task AddCustomer(Customer customer);

        /// <summary>
        /// Removes the customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        Task RemoveCustomer(int customerId);
    }
}
