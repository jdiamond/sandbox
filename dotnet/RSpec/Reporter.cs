using System;
using System.Diagnostics;

namespace RSpec
{
    public class Reporter
    {
        private Stopwatch _stopwatch;
        private int _exampleCount;
        private int _failureCount;
        private int _pendingCount;

        public void Start(ExampleGroup exampleGroup)
        {
            if (_stopwatch == null)
            {
                _stopwatch = Stopwatch.StartNew();
            }

            Console.WriteLine(exampleGroup.Subject);
        }

        public void Start(Example example)
        {
            _exampleCount++;

            Console.Write("  {0}", example.Description);
        }

        public void Pass(Example example)
        {
            Console.WriteLine();
        }

        public void Fail(Example example)
        {
            _failureCount++;

            Console.WriteLine(" (FAILED)");
        }

        public void Pending(Example example)
        {
            _pendingCount++;

            Console.WriteLine(" (PENDING)");
        }

        public void Summarize()
        {
            _stopwatch.Stop();

            Console.WriteLine();

            Console.WriteLine("Finished in {0} seconds", _stopwatch.ElapsedMilliseconds / 1000.0);

            string counts = string.Format("{0} examples, {1} failures", _exampleCount, _failureCount);

            if (_pendingCount > 0)
            {
                counts += string.Format(", {0} pending", _pendingCount);
            }

            Console.WriteLine(counts);
        }
    }
}