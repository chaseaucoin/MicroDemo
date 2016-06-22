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
        /// Gets the item with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<string> Get(string key)
        {
            return await Task.FromResult("Not implemented");
        }

        /// <summary>
        /// Adds or updates an item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        [HttpPost]
        public async Task AddOrUpdate(string key, [FromBody]string value)
        {
            await Task.FromResult("Not implemented");
        }
    }
}
