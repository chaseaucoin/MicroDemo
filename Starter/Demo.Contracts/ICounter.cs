using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Counter.Contracts
{
    [DataContract]
    public class CounterStats
    {
           [DataMember]
        public string ServedBy { get; set; }

        [DataMember]
        public long ClickCount { get; set; }
    }
        
    public interface ICounter : Microsoft.ServiceFabric.Services.Remoting.IService
    {
        Task<CounterStats> GetClickCount();

        Task<CounterStats> IncrementClickCount();
    }
}
