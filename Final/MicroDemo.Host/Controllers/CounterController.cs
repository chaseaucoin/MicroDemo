using Demo.Contracts;
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
        [HttpGet]
        public async Task<CounterStats> GetClickCount()
        {
            try
            {
                var counter = DemoProxy.CounterService();

                var result = await counter.GetClickCount();

                return result;
            }
            catch(Exception ex)
            {
                return new CounterStats()
                {
                    ClickCount = 0,
                    ServedBy = ex.Message
                };
            }            
        }

        [HttpGet]
        public async Task<CounterStats> IncrementClickCount()
        {
            try
            {
                var counter = DemoProxy.CounterService();

                var result = await counter.IncrementClickCount();

                return result;
            }
            catch (Exception ex)
            {
                return new CounterStats()
                {
                    ClickCount = 0,
                    ServedBy = ex.Message
                };
            }
        }
    }
}
