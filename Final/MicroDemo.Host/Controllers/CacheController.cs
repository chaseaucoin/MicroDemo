using Demo.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MicroDemo.Host.Controllers
{
    public class CacheController : ApiController
    {
        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<string> Get(string key)
        {
            var service = DemoProxy.CacheService(key);
            var value = await service.Get(key);

            return value;
        }

        /// <summary>
        /// Adds the or update.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddOrUpdate(string key, [FromBody]string value)
        {
            var service = DemoProxy.CacheService(key);

            await service.AddOrUpdate(key, value);
        }
    }
}
