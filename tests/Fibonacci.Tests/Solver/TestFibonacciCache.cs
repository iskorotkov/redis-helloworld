using System.Collections.Generic;
using Fibonacci.Cache;

namespace Fibonacci.Tests.Solver
{
    public class TestFibonacciCache : IFibonacciCache
    {
        private readonly Dictionary<int, ulong> _cache = new Dictionary<int, ulong>();

        public void Save(int index, ulong value)
        {
            _cache.Add(index, value);
        }

        public (bool Exists, ulong Value) Retrieve(int index)
        {
            var exists = _cache.TryGetValue(index, out var value);
            return (exists, value);
        }
    }
}
