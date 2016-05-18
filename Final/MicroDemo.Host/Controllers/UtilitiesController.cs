using Demo.Contracts.Counter;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using System;
using System.Fabric;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroDemo.Host.Controllers
{
    public class UtilitiesController : ApiController
    {
        [HttpGet]
        // GET api/Utilities/WhoIs 
        public string WhoIs()
        {
            return FabricRuntime.GetNodeContext().NodeName;
        }
    }
}
