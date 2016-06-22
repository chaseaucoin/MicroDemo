using Demo.Contracts.Customers;
using Demo.Contracts.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroDemo.Host.Controllers
{
    public class InvoiceController : ApiController
    {
        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Invoice> GetInvoice(int id)
        {
            return await Task.FromResult<Invoice>(null);            
        }

        /// <summary>
        /// Gets the customers.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Invoice>> GetAllInvoicesForCustomer(int customerId)
        {
            return await Task.FromResult<List<Invoice>>(null);
        }

        /// <summary>
        /// Count of Customers.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateInvoice(Invoice invoice)
        {
            await Task.FromResult(0);
        }

        /// <summary>
        /// Adds the invoice.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns>Invoice Id</returns>
        [HttpPost]
        public async Task<int> AddInvoice(int customerId)
        {
            return await Task.FromResult(0);
        }

        public async Task<decimal> GetInvoicesTotal()
        {
            return await Task.FromResult(0);
        }
    }
}
