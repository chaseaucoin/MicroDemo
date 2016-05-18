using System.Runtime.Serialization;

namespace Demo.Contracts.Counter
{
    [DataContract]
    public class CounterStats
    {
        [DataMember]
        public string ServedBy { get; set; }

        [DataMember]
        public long ClickCount { get; set; }
    }
}
