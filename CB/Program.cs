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
            var breaker = new TestBreaker(new CircuitBreaker(1, TimeSpan.FromMilliseconds(50), TimeSpan.FromMilliseconds(500)));
            foreach(var i in Enumerable.Range(0,100))
            {
                TestCase(breaker);
                Console.WriteLine();
            }
        }

        private static Random random = new Random();
        private static void TestCase(TestBreaker breaker)
        {
            try
            {
                string result;

                if (random.Next(10) == 0)
                {
                    result = breaker.Instance.WithSyncCircuitBreaker(() => { throw new Exception("Test"); return "Test"; });
                }
                else
                {
                    result = breaker.Instance.WithSyncCircuitBreaker(() => "Test");
                }
                Console.WriteLine(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Thread.Sleep(100);
        }
    }
    

    public class TestBreaker
    {
        public CountdownEvent HalfOpenLatch { get; private set; }
        public CountdownEvent OpenLatch { get; private set; }
        public CountdownEvent ClosedLatch { get; private set; }
        public CircuitBreaker Instance { get; private set; }

        public TestBreaker(CircuitBreaker instance)
        {
            HalfOpenLatch = new CountdownEvent(1);
            OpenLatch = new CountdownEvent(1);
            ClosedLatch = new CountdownEvent(1);
            Instance = instance;
            Instance.OnClose(() => { Console.WriteLine("Close"); if (!ClosedLatch.IsSet) ClosedLatch.Signal(); })
                    .OnHalfOpen(() => { Console.WriteLine("HalfOpen"); if (!HalfOpenLatch.IsSet) HalfOpenLatch.Signal(); })
                    .OnOpen(() => { Console.WriteLine("Open"); if (!OpenLatch.IsSet) OpenLatch.Signal(); });
        }


    }
}
