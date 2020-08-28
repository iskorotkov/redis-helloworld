using Fibonacci.Cache;
using Shouldly;
using StackExchange.Redis;
using Xunit;

namespace Fibonacci.Tests.Cache
{
    public class RedisCacheTests
    {
        private readonly IDatabase _db;

        public RedisCacheTests()
        {
            _db = ConnectionMultiplexer.Connect("localhost").GetDatabase();
        }

        private RedisCache CreateCache(string testName)
        {
            return new RedisCache(_db, testName);
        }

        private static void FeedData(IFibonacciCache cache)
        {
            cache.Save(0, 1);
            cache.Save(1, 90);
            cache.Save(-10, 33);
        }

        [Fact]
        private void ValuesShouldBeSaved()
        {
            var cache = CreateCache(nameof(ValuesShouldBeSaved));
            FeedData(cache);

            cache.Retrieve(-10).ShouldBe((true, 33ul));
            cache.Retrieve(0).ShouldBe((true, 1ul));
            cache.Retrieve(1).ShouldBe((true, 90ul));
        }

        [Fact]
        private void NoExtraValuesShouldBeSaved()
        {
            var cache = CreateCache(nameof(ValuesShouldBeSaved));
            FeedData(cache);

            for (var i = -1000; i < 1000; i++)
            {
                var wasFeed = i == 0 || i == 1 || i == -10;
                cache.Retrieve(i).Exists.ShouldBe(wasFeed);
            }
        }

        [Fact]
        private void ValuesShouldBeCleared()
        {
            var cache = CreateCache(nameof(ValuesShouldBeCleared));
            FeedData(cache);
            cache.Reset();

            for (var i = -1000; i < 1000; i++)
            {
                cache.Retrieve(i).Exists.ShouldBeFalse();
            }
        }
    }
}
