using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using InvoiceAnalytics.Interfaces;

namespace InvoiceAnalytics
{
    /// <remarks>
    /// This class represents an actor.
    /// Every ActorID maps to an instance of this class.
    /// The StatePersistence attribute determines persistence and replication of actor state:
    ///  - Persisted: State is written to disk and replicated.
    ///  - Volatile: State is kept in memory only and replicated.
    ///  - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class InvoiceAnalytics : Actor, IInvoiceAggregator
    {
        public InvoiceAnalytics(ActorService actorService, ActorId actorId) : base(actorService, actorId)
        {
        }

        public Task AddToInvoiceTotal(decimal amount)
        {
            return this.StateManager.AddOrUpdateStateAsync("InvoiceTotals", amount, (key, value) => {
                return value + amount;
            });
        }

        public Task<decimal> GetInvoiceTotals()
        {
            return this.StateManager.GetStateAsync<decimal>("InvoiceTotals");
        }

        /// <summary>
        /// This method is called whenever an actor is activated.
        /// An actor is activated the first time any of its methods are invoked.
        /// </summary>
        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");

            // The StateManager is this actor's private state store.
            // Data stored in the StateManager will be replicated for high-availability for actors that use volatile or persisted state storage.
            // Any serializable object can be saved in the StateManager.
            // For more information, see http://aka.ms/servicefabricactorsstateserialization

            return this.StateManager.TryAddStateAsync("InvoiceTotals", (decimal)0.0);
        }
    }
}
