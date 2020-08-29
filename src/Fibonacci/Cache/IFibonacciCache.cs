using System.Numerics;

namespace Fibonacci.Cache
{
    public interface IFibonacciCache
    {
        void Save(int index, BigInteger value);
        (bool Exists, BigInteger Value) Retrieve(int index);
        void Reset();
    }
}
