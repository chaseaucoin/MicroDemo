using Demo.Contracts.Cache;
using Demo.Contracts.Counter;
using Demo.Contracts.Customers;
using Demo.Contracts.Invoices;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Contracts
{
    public static class DemoProxy
    {
        public static ICustomerService CustomerService()
        {
            var uri = new Uri("fabric:/MicroDemo/CustomerService");            
            var service = ServiceProxy.Create<ICustomerService>(uri, new ServicePartitionKey(0));

            return service;
        }

        public static IInvoiceService InvoiceService()
        {
            var uri = new Uri("fabric:/MicroDemo/InvoiceService");
            var service = ServiceProxy.Create<IInvoiceService>(uri, new ServicePartitionKey(0));

            return service;
        }

        public static ICounterService CounterService()
        {
            var uri = new Uri("fabric:/MicroDemo/Counter");
            var service = ServiceProxy.Create<ICounterService>(uri, new ServicePartitionKey(0));
            
            return service;
        }

        public static ICacheService CacheService(string key)
        {
            long partitions = 5;
            var selectedPartition = QuickHash.Hash(key) % partitions;

            var uri = new Uri("fabric:/MicroDemo/CacheService");
            var service = ServiceProxy.Create<ICacheService>(uri, new ServicePartitionKey(0));

            return service;
        }
    }
}
