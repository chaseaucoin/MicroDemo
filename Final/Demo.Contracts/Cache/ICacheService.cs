using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Contracts.Cache
{
    public interface ICacheService : Microsoft.ServiceFabric.Services.Remoting.IService
    {
        Task<string> Get(string key);

        Task AddOrUpdate(string key, string value);
    }
}
