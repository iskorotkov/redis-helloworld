namespace Fibonacci.Cache
{
    public interface IFibonacciCache
    {
        void Save(int index, ulong value);
        (bool Exists, ulong Value) Retrieve(int index);
    }
}
