using System.Numerics;
using Fibonacci.Cache;

namespace Fibonacci.Solver
{
    public class FibonacciSolver
    {
        private readonly IFibonacciCache _cache;

        public FibonacciSolver(IFibonacciCache cache)
        {
            _cache = cache;
        }

        public BigInteger At(int index)
        {
            if (index <= 0)
            {
                return 0;
            }

            if (index == 1)
            {
                return 1;
            }

            return _cache.Retrieve(index) switch
            {
                (true, var cached) => cached,
                _ => Calculate(index)
            };
        }

        private BigInteger Calculate(int index)
        {
            var value = At(index - 2) + At(index - 1);
            _cache.Save(index, value);
            return value;
        }
    }
}
