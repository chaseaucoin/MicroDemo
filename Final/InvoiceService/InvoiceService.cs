using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Demo.Contracts.Invoices;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;

namespace InvoiceService
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class InvoiceService : StatefulService, IInvoiceService
    {
        public InvoiceService(StatefulServiceContext context)
            : base(context)
        { }

        public async Task<int> AddInvoice(int customerId)
        {
            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<int, List<Invoice>>>("InvoiceData");

            using (var tx = this.StateManager.CreateTransaction())
            {
                var result = await myDictionary.TryGetValueAsync(tx, 0);
                var invoices = result.Value;

                var newId = invoices.Max(x => x.Id) + 1;
                invoices.Add(new Invoice() { Id = newId, CustomerId = customerId });

                await myDictionary.TryUpdateAsync(tx, 0, invoices, invoices);

                await tx.CommitAsync();

                return newId;
            }
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoices()
        {
            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<int, List<Invoice>>>("InvoiceData");

            using (var tx = this.StateManager.CreateTransaction())
            {
                var result = await myDictionary.TryGetValueAsync(tx, 0);
                var selectedInvoices = result.Value;

                return selectedInvoices;
            }
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoicesForCustomer(int customerId)
        {
            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<int, List<Invoice>>>("InvoiceData");

            using (var tx = this.StateManager.CreateTransaction())
            {
                var result = await myDictionary.TryGetValueAsync(tx, 0);
                var selectedInvoices = result.Value
                    .Where(invoice => invoice.CustomerId == customerId);

                return selectedInvoices;
            }
        }

        public async Task<Invoice> GetInvoice(int Id)
        {
            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<int, List<Invoice>>>("InvoiceData");

            using (var tx = this.StateManager.CreateTransaction())
            {
                var result = await myDictionary.TryGetValueAsync(tx, 0);
                var selectedInvoice = result.Value
                    .FirstOrDefault(invoice => invoice.Id == Id);

                return selectedInvoice;
            }
        }

        public async Task<decimal> GetInvoiceTotals()
        {
            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<int, List<Invoice>>>("InvoiceData");

            using (var tx = this.StateManager.CreateTransaction())
            {
                var result = await myDictionary.TryGetValueAsync(tx, 0);
                var sum = result.Value.Sum(x => x.InvoiceTotal);

                return sum;
            }
        }

        public async Task UpdateInvoice(Invoice invoice)
        {
            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<int, List<Invoice>>>("InvoiceData");

            using (var tx = this.StateManager.CreateTransaction())
            {
                var result = await myDictionary.TryGetValueAsync(tx, 0);
                var invoices = result.Value;

                var selectedInvoice = result.Value
                    .FirstOrDefault(_invoice => _invoice.Id == invoice.Id);

                selectedInvoice.Items = invoice.Items;

                await myDictionary.TryUpdateAsync(tx, 0, invoices, invoices);

                await tx.CommitAsync();
            }
        }


        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see http://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new[] {
                new ServiceReplicaListener(context =>
                    this.CreateServiceRemotingListener(context))
            };
        }

        /// <summary>
        /// This is the main entry point for your service replica.
        /// This method executes when this replica of your service becomes primary and has write status.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service replica.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<int, List<Invoice>>>("InvoiceData");

            using (var tx = this.StateManager.CreateTransaction())
            {
                var result = await myDictionary.TryGetValueAsync(tx, 0);

                if (!result.HasValue)
                {
                    var defaultInvoices = new List<Invoice>()
                    {
                        new Invoice() { Id = 0, CustomerId = 0,
                            Items = new List<InvoiceItem>(){
                                new InvoiceItem() { Name = "Girl Scout Cookies", Price = 3.5m, Quantity = 6 }
                            }
                        }
                    };

                    await myDictionary.AddAsync(tx, 0, defaultInvoices);
                }

                // If an exception is thrown before calling CommitAsync, the transaction aborts, all changes are 
                // discarded, and nothing is saved to the secondary replicas.
                await tx.CommitAsync();
            }

            await base.RunAsync(cancellationToken);
        }
    }
}
