using System.Threading.Tasks;

namespace Demo.Contracts.Counter
{
    public interface ICounterService : Microsoft.ServiceFabric.Services.Remoting.IService
    {
        Task<CounterStats> GetClickCount();

        Task<CounterStats> IncrementClickCount();
    }
}
