using Demo.Contracts.Counter;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using System;
using System.Fabric;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroDemo.Host.Controllers
{
    public class CounterController : ApiController
    {
        // GET api/values 
        /// <summary>
        /// Gets the click count.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        [HttpGet]
        public async Task<CounterStats> GetClickCount()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Increments the click count.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        [HttpGet]
        public async Task<CounterStats> IncrementClickCount()
        {
            throw new NotImplementedException();
        }
    }
}
