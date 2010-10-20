using System;
using System.Diagnostics;

namespace RSpec
{
    public class Reporter
    {
        private Stopwatch _stopwatch;
        private int _level;
        private int _nonChildExampleGroupCount;
        private int _exampleCount;
        private int _failureCount;
        private int _pendingCount;

        public void RunStarted()
        {
            _stopwatch = Stopwatch.StartNew();
            _level = 0;
        }

        public void ExampleGroupStarted(ExampleGroup exampleGroup)
        {
            if (exampleGroup.Parent == null)
            {
                if (_nonChildExampleGroupCount > 0)
                {
                    Console.WriteLine();
                }

                _nonChildExampleGroupCount++;
            }

            Console.WriteLine("{0}{1}", Indent(), exampleGroup.Subject);

            _level++;
        }

        public void ExampleStarted(Example example)
        {
            _exampleCount++;

            Console.Write("{0}{1}", Indent(), example.Description);
        }

        public void ExamplePassed(Example example)
        {
            Console.WriteLine();
        }

        public void ExampleFailed(Example example)
        {
            _failureCount++;

            Console.WriteLine(" (FAILED)");
        }

        public void ExamplePending(Example example)
        {
            _pendingCount++;

            Console.WriteLine(" (PENDING)");
        }

        public void RunFinished()
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

        private string Indent()
        {
            return new string(' ', _level * 2);
        }
    }
}