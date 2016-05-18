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
        public Task<Invoice> GetInvoice(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the customers.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Invoice>> GetAllInvoicesForCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Count of Customers.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Task UpdateInvoice(Invoice invoice)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds the invoice.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns>Invoice Id</returns>
        [HttpPost]
        public Task<int> AddInvoice(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetInvoicesTotal()
        {
            throw new NotImplementedException();
        }
    }
}
