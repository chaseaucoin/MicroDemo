using System.Collections.Generic;
using System.Fabric;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Demo.Contracts.Counter;

namespace Keyhole.Project.Counter
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class CounterService : StatefulService, ICounterService
    {
        public CounterService(StatefulServiceContext context)
            : base(context)
        { }

        public async Task<CounterStats> GetClickCount()
        {
            using (var tx = StateManager.CreateTransaction())
            {
                var countDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<long, long>>("countDictionary");
                var result = await countDictionary.TryGetValueAsync(tx, 0);

                await tx.CommitAsync();
                return new CounterStats
                {
                    ServedBy = FabricRuntime.GetNodeContext().NodeName,
                    ClickCount = result.HasValue ? result.Value : 0
                };
            }
        }

        public async Task<CounterStats> IncrementClickCount()
        {
            using (var tx = StateManager.CreateTransaction())
            {
                var countDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<long, long>>("countDictionary");
                var result = await countDictionary.AddOrUpdateAsync(tx, 0, 1, (key, value) => ++value);

                await tx.CommitAsync();
                return new CounterStats
                {
                    ServedBy = FabricRuntime.GetNodeContext().NodeName,
                    ClickCount = result
                };
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
    }
}
