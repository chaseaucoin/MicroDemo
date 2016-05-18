using System;

namespace CacheService
{
    internal class CacheItemPolicy
    {
        public CacheItemPolicy()
        {
        }

        public DateTimeOffset AbsoluteExpiration { get; set; }
    }
}