using System;
using System.Diagnostics;

namespace BothMarshalingTypes
{
    public static class Program
    {
        private static void Test(string name, Action action)
        {
            var sw = new Stopwatch();

            Console.WriteLine($"{name}: Start.");
            sw.Start();

            action();

            sw.Stop();
            Console.WriteLine($"{name}: Finished, Elapsed={sw.ElapsedMilliseconds}msec");
        }

        public static void Main(string[] args)
        {
            Test(nameof(MarshalingTest.AutoMarshaling), MarshalingTest.AutoMarshaling);
            Test(nameof(MarshalingTest.ManualMarshaling), MarshalingTest.ManualMarshaling);
        }
    }
}
