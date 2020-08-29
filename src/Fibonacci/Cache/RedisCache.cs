using System.Numerics;
using StackExchange.Redis;

namespace Fibonacci.Cache
{
    public class RedisCache : IFibonacciCache
    {
        private readonly IDatabase _db;
        private readonly string _cacheKey;

        public RedisCache(IDatabase db, string cacheKey)
        {
            _db = db;
            _cacheKey = cacheKey;
        }

        public void Save(int index, BigInteger value)
        {
            _db.HashSet(_cacheKey, index, value.ToString());
        }

        public (bool Exists, BigInteger Value) Retrieve(int index)
        {
            var redisValue = _db.HashGet(_cacheKey, index);
            return redisValue.HasValue switch
            {
                true => (true, ulong.Parse(redisValue)),
                false => (false, 0)
            };
        }

        public void Reset()
        {
            _db.KeyDelete(_cacheKey);
        }
    }
}
