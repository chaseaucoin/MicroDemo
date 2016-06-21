using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Contracts.Invoices
{
    public interface IInvoiceService : Microsoft.ServiceFabric.Services.Remoting.IService
    {
        /// <summary>
        /// Gets the invoice.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<Invoice> GetInvoice(int id);

        /// <summary>
        /// Gets all invoices.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Invoice>> GetAllInvoices();

        /// <summary>
        /// Gets all invoices for customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        Task<IEnumerable<Invoice>> GetAllInvoicesForCustomer(int customerId);

        /// <summary>
        /// Gets the invoice totals.
        /// </summary>
        /// <returns></returns>
        Task<decimal> GetInvoiceTotals();

        /// <summary>
        /// Adds the invoice for a customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        Task<int> AddInvoice(int customerId);

        /// <summary>
        /// Updates the invoice.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        /// <returns></returns>
        Task UpdateInvoice(Invoice invoice);
    }
}
