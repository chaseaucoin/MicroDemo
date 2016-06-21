using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Demo.Contracts.Cache;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using System.Runtime.Caching;

namespace CacheService
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class CacheService : StatefulService, ICacheService
    {
        public CacheService(StatefulServiceContext context)
            : base(context)
        { }

        public Task AddOrUpdate(string key, string value)
        {
            MemoryCache.Default[key] = value;

            return Task.FromResult(true);
        }

        public Task<string> Get(string key)
        {
            string result = null;

            if (MemoryCache.Default.Contains(key))
            {
                result = MemoryCache.Default[key] as string;
            }

            return Task.FromResult(result);
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
