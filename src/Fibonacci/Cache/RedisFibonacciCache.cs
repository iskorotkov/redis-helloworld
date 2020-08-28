using StackExchange.Redis;

namespace Fibonacci.Cache
{
    public class RedisFibonacciCache : IFibonacciCache
    {
        private readonly IDatabase _db;
        private readonly string _cacheKey;

        public RedisFibonacciCache(IDatabase db, string cacheKey)
        {
            _db = db;
            _cacheKey = cacheKey;
        }

        public void Save(int index, ulong value)
        {
            _db.HashSet(_cacheKey, index, value);
        }

        public (bool Exists, ulong Value) Retrieve(int index)
        {
            var redisValue = _db.HashGet(_cacheKey, index);
            return redisValue.HasValue switch
            {
                true => (true, ulong.Parse(redisValue)),
                false => (false, 0)
            };
        }
    }
}
