using Demo.Contracts;
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
            var service = DemoProxy.InvoiceService();
            var result = await service.GetInvoice(id);

            return result;
        }

        /// <summary>
        /// Gets the customers.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Invoice>> GetAllInvoicesForCustomer(int customerId)
        {
            var service = DemoProxy.InvoiceService();
            var result = await service.GetAllInvoicesForCustomer(customerId);

            return result;
        }

        /// <summary>
        /// Count of Customers.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateInvoice(Invoice invoice)
        {
            var service = DemoProxy.InvoiceService();
            await service.UpdateInvoice(invoice);
        }

        /// <summary>
        /// Adds the invoice.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns>Invoice Id</returns>
        [HttpPost]
        public async Task<int> AddInvoice(int customerId)
        {
            var service = DemoProxy.InvoiceService();
            var invoiceId = await service.AddInvoice(customerId);

            return invoiceId;
        }

        public async Task<decimal> GetInvoicesTotal()
        {
            var aggregator = DemoProxy.InvoiceAggregator("AllInvoices");            
            var totals = await aggregator.GetInvoiceTotals();

            return totals;
        }
    }
}
