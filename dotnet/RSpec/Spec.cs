using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RSpec
{
    public class Spec
    {
        private static Action _beforeEach;
        private static ExampleGroup _exampleGroup;

        protected static void BeforeEach(Action action)
        {
            _beforeEach = action;

            // TODO: Allow BeforeEach to appear after Describe?
        }

        protected static void Describe(string subject, Action block)
        {
            _exampleGroup = new ExampleGroup(subject)
                                {
                                    BeforeEach = _beforeEach
                                };
            block();
        }

        protected static void It(string description)
        {
            It(description, () => { throw new NotImplementedException(); });
        }

        protected static void It(string description, Action block)
        {
            if (_exampleGroup == null)
            {
                throw new Exception("You need to use Describe first!");
            }

            _exampleGroup.Examples.Add(new Example(description, block));
        }

        protected static void Assert(Func<bool> condition)
        {
            // TODO: Use Verify.That for better failure messages.

            if (!condition())
            {
                throw new Exception("Failed assertion!");
            }
        }

        public static void Run()
        {
            var reporter = new Reporter();

            _exampleGroup.Run(reporter);

            reporter.Summarize();
        }
    }

    public class ExampleGroup
    {
        public ExampleGroup(string subject)
        {
            Subject = subject;
            Examples = new List<Example>();
        }

        public string Subject { get; set; }
        public Action BeforeEach { get; set; }

        public List<Example> Examples { get; set; }

        public void Run(Reporter reporter)
        {
            reporter.Start(this);

            foreach (var example in Examples)
            {
                if (BeforeEach != null)
                {
                    BeforeEach();
                }

                example.Run(reporter);
            }
        }
    }

    public class Example
    {
        public string Description { get; set; }
        public Action Block { get; set; }

        public Example(string description, Action block)
        {
            Description = description;
            Block = block;
        }

        public void Run(Reporter reporter)
        {
            reporter.Start(this);

            try
            {
                Block();

                reporter.Pass(this);
            }
            catch (NotImplementedException)
            {
                reporter.Pending(this);
            }
            catch (Exception)
            {
                reporter.Fail(this);
            }
        }
    }

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