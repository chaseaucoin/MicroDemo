using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;

namespace InvoiceAnalytics.Interfaces
{
    /// <summary>
    /// This interface defines the methods exposed by an actor.
    /// Clients use this interface to interact with the actor that implements it.
    /// </summary>
    public interface IInvoiceAggregator : IActor
    {
        /// <summary>
        /// TODO: Replace with your own actor method.
        /// </summary>
        /// <returns></returns>
        Task<decimal> GetInvoiceTotals();

        /// <summary>
        /// TODO: Replace with your own actor method.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        Task AddToInvoiceTotal(decimal amount);
    }
}
