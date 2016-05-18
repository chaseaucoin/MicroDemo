using CupcakeFactory.SimpleProxy;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Polly;
using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Permissions;

namespace MicroDemo.Host
{
    public class CircuitProxy<T> where T : IService
    {
        SimpleProxy<T> _proxy;
        T _fabricProxy;
        Dictionary<MethodInfo, CircuitBreakerPolicy> _breakers;

        [PermissionSet(SecurityAction.LinkDemand)]
        public CircuitProxy()
        {
            _proxy = new SimpleProxy<T>(ProxyWithBreaker);
            _breakers = new Dictionary<MethodInfo, CircuitBreakerPolicy>();
        }

        public T CreateProxy(Uri serviceUri, ServicePartitionKey partitionKey = null)
        {
            foreach (var method in typeof(T).GetMethods())
            {
                var policy = Policy
                    .Handle<Exception>()
                    .AdvancedCircuitBreaker(
                        failureThreshold: 0.5, // Break on >=50% actions result in handled exceptions...
                        samplingDuration: TimeSpan.FromSeconds(10), // ... over any 10 second period
                        minimumThroughput: 8, // ... provided at least 8 actions in the 10 second period.
                        durationOfBreak: TimeSpan.FromSeconds(30) // Break for 30 seconds.
                                );

                _breakers.Add(method, policy);
            }

            _fabricProxy = ServiceProxy.Create<T>(serviceUri, partitionKey);

            return (T)_proxy.GetTransparentProxy();
        }

        private object ProxyWithBreaker(MethodBase methodBase, MethodParameterCollection parameterCollection)
        {
            object returnObj = null;

            var methodInfo = methodBase as MethodInfo;

            _breakers[methodInfo].Execute(() =>
            {
                returnObj = methodBase.Invoke(_fabricProxy, parameterCollection.Args);
            });

            return returnObj;
        }
    }
}
