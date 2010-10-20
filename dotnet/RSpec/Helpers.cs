using System;

namespace RSpec
{
    public class Helpers
    {
        private static Action _beforeEach;

        protected static void BeforeEach(Action action)
        {
            _beforeEach = action;
        }

        protected static void Describe(string subject, Action action)
        {
            Console.WriteLine(subject);
            action();
        }

        protected static void It(string description, Action action)
        {
            Console.WriteLine("  {0}", description);
            if (_beforeEach != null) _beforeEach();
            action();
        }

        protected static void Assert(Func<bool> condition)
        {
            if (!condition())
                throw new Exception("Assertion failed!");
        }
    }
}