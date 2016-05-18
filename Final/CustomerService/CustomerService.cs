using System;
using System.Linq;
using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Demo.Contracts.Customers;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;

namespace CustomerService
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class CustomerService : StatefulService, ICustomerService
    {
        public CustomerService(StatefulServiceContext context)
            : base(context)
        { }

        public async Task AddCustomer(Customer customer)
        {
            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<int, List<Customer>>>("CustomerData");

            using (var tx = this.StateManager.CreateTransaction())
            {
                var result = await myDictionary.TryGetValueAsync(tx, 0);
                var customers = result.Value;

                var newId = customers.Max(x => x.Id) + 1;
                customer.Id = newId;

                customers.Add(customer);

                await myDictionary.TryUpdateAsync(tx, 0, customers, customers);

                await tx.CommitAsync();
            }
        }

        public async Task<int> CustomerCount()
        {
            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<int, List<Customer>>>("CustomerData");

            using (var tx = this.StateManager.CreateTransaction())
            {
                var result = await myDictionary.TryGetValueAsync(tx, 0);
                var count = result.Value.Count;
                
                return count;
            }
        }
        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<int, List<Customer>>>("CustomerData");

            using (var tx = this.StateManager.CreateTransaction())
            {
                var result = await myDictionary.TryGetValueAsync(tx, 0);
                
                return result.Value;
            }
        }

        public async Task<Customer> GetCustomer(int id)
        {
            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<int, List<Customer>>>("CustomerData");

            using (var tx = this.StateManager.CreateTransaction())
            {
                var result = await myDictionary.TryGetValueAsync(tx, 0);
                var selectedCustomer = result.Value.FirstOrDefault(customer => customer.Id == id);

                return selectedCustomer;
            }
        }

        public Task RemoveCustomer(int customerId)
        {
            throw new NotImplementedException();
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

            var myDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<int, List<Customer>>>("CustomerData");

            using (var tx = this.StateManager.CreateTransaction())
            {
                var result = await myDictionary.TryGetValueAsync(tx, 0);

                if(!result.HasValue)
                {
                    var defaultCustomers = new List<Customer>()
                    {
                        new Customer() { Id = 0, FirstName = "John", LastName = "Doe", Age = 42},
                        new Customer() { Id = 1, FirstName = "Jane", LastName = "Doe", Age = 36},
                    };

                    await myDictionary.AddAsync(tx, 0, defaultCustomers);
                }

                // If an exception is thrown before calling CommitAsync, the transaction aborts, all changes are 
                // discarded, and nothing is saved to the secondary replicas.
                await tx.CommitAsync();
            }
        }
    }
}
