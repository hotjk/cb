using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CB
{
    class Program
    {
        static void Main(string[] args)
        {
            var breaker = new CircuitBreaker(2, TimeSpan.FromMilliseconds(50), TimeSpan.FromMilliseconds(500));
            breaker.OnClose(() => { Console.WriteLine("Close"); })
                    .OnHalfOpen(() => { Console.WriteLine("HalfOpen");  })
                    .OnOpen(() => {  Console.WriteLine("Open"); });

            foreach (var i in Enumerable.Range(0,10000000))
            {
                TestCaseAsync(breaker);
                Thread.Sleep(10);
            }
        }

        private static Random random = new Random();

        private static void TestCase(CircuitBreaker breaker)
        {
            try
            {
                string result;
                if (random.Next(10) == 0)
                {
                    result = breaker.WithSyncCircuitBreaker(() => { throw new Exception("Test"); return "Test"; });
                }
                else
                {
                    result = breaker.WithSyncCircuitBreaker(() => { Task.Delay(100); return "Test"; });
                }
                Console.WriteLine(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Thread.Sleep(1000);
        }

        private static async void TestCaseAsync(CircuitBreaker breaker)
        {
            try
            {
                await Task.Delay(100);
                string result;
                if (random.Next(10) == 0)
                {
                    result = await breaker.WithCircuitBreaker<string>(() => { throw new Exception("Business Exception"); Task.FromResult("Test"); });
                }
                else
                {
                    result = await breaker.WithCircuitBreaker<string>(() => Task.FromResult("Test"));
                }
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
