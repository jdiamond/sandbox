using System;

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
}