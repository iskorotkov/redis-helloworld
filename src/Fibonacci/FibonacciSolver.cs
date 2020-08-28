using StackExchange.Redis;

namespace Fibonacci
{
    public class FibonacciSolver
    {
        private readonly IDatabase _db;

        public FibonacciSolver(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
        }

        public ulong At(int index)
        {
            if (index <= 0)
            {
                return 0;
            }

            if (index == 1)
            {
                return 1;
            }

            var redisValue = _db.HashGet("fibonacci", index);
            if (redisValue.HasValue)
            {
                return ulong.Parse(redisValue);
            }

            var value = At(index - 2) + At(index - 1);
            _db.HashSet("fibonacci", index, value);
            return value;
        }
    }
}
