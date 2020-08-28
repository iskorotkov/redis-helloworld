using Fibonacci.Solver;
using Shouldly;
using Xunit;

namespace Fibonacci.Tests.Solver
{
    public class FibonacciSolverTests
    {
        private static FibonacciSolver CreateSolver()
        {
            var cache = new TestFibonacciCache();
            return new FibonacciSolver(cache);
        }

        [Fact]
        public void NegativeIndexesShouldReturnZero()
        {
            var solver = CreateSolver();
            solver.At(-1).ShouldBe(0ul);
            solver.At(-2).ShouldBe(0ul);
            solver.At(-100).ShouldBe(0ul);
        }

        [Fact]
        public void ZeroIndexShouldReturnZero()
        {
            CreateSolver().At(0).ShouldBe(0ul);
        }

        [Fact]
        public void PositiveIndexesShouldReturnFibonacciNumbers()
        {
            var solver = CreateSolver();
            var x = 0ul;
            var y = 1ul;
            for (var i = 1; i <= 1000; i++)
            {
                solver.At(i).ShouldBe(y);

                var sum = x + y;
                x = y;
                y = sum;
            }
        }
    }
}
