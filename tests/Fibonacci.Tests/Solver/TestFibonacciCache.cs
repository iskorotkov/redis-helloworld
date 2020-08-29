using System.Collections.Generic;
using System.Numerics;
using Fibonacci.Cache;

namespace Fibonacci.Tests.Solver
{
    public class TestFibonacciCache : IFibonacciCache
    {
        private readonly Dictionary<int, BigInteger> _cache = new Dictionary<int, BigInteger>();

        public void Save(int index, BigInteger value)
        {
            _cache.Add(index, value);
        }

        public (bool Exists, BigInteger Value) Retrieve(int index)
        {
            var exists = _cache.TryGetValue(index, out var value);
            return (exists, value);
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }
    }
}
