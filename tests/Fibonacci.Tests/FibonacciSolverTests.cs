using Shouldly;
using StackExchange.Redis;
using Xunit;

namespace Fibonacci.Tests
{
    public class FibonacciSolverTests
    {
        private readonly FibonacciSolver _solver;

        public FibonacciSolverTests()
        {
            var redis = ConnectionMultiplexer.Connect("localhost");
            _solver = new FibonacciSolver(redis);
        }

        [Fact]
        public void NegativeIndexesShouldReturnZero()
        {
            _solver.At(-1).ShouldBe(0ul);
            _solver.At(-2).ShouldBe(0ul);
            _solver.At(-100).ShouldBe(0ul);
        }

        [Fact]
        public void ZeroIndexShouldReturnZero()
        {
            _solver.At(0).ShouldBe(0ul);
        }

        [Fact]
        public void PositiveIndexesShouldReturnFibonacciNumbers()
        {
            var x = 0ul;
            var y = 1ul;
            for (var i = 1; i <= 1000; i++)
            {
                _solver.At(i).ShouldBe(y);

                var sum = x + y;
                x = y;
                y = sum;
            }
        }
    }
}
