using System;
using Fibonacci.Cache;
using Fibonacci.Solver;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Fibonacci
{
    internal static class Program
    {
        private static void Main()
        {
            var provider = new ServiceCollection()
                .ConfigureServices()
                .BuildServiceProvider();

            using var scope = provider.CreateScope();
            RunApp(scope.ServiceProvider);
        }

        private static void RunApp(IServiceProvider provider)
        {
            var solver = provider.GetRequiredService<FibonacciSolver>();
            for (;;)
            {
                var line = Console.ReadLine() ?? throw new ArgumentNullException();
                var index = int.Parse(line);
                var result = solver.At(index);
                Console.WriteLine($" => {result}");
            }
        }

        private static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect("localhost"));
            services.AddSingleton<IFibonacciCache, RedisCache>(provider =>
            {
                var database = provider.GetRequiredService<IConnectionMultiplexer>().GetDatabase();
                const string cacheKey = "fibonacci";
                return new RedisCache(database, cacheKey);
            });
            services.AddTransient<FibonacciSolver>();

            return services;
        }
    }
}
