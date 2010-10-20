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

            // TODO: Print summary here?
        }

        protected static void It(string description, Action action)
        {
            Console.Write("  {0}", description);

            if (_beforeEach != null)
            {
                _beforeEach();
            }

            try
            {
                action();
            }
            catch (NotImplementedException)
            {
                Console.Write(" (PENDING)");
            }
            catch (Exception)
            {
                Console.Write(" (FAILED)");
            }

            Console.WriteLine();
        }

        protected static void It(string description)
        {
            It(description, () => { throw new NotImplementedException(); });
        }

        protected static void Assert(Func<bool> condition)
        {
            // TODO: Use Verify.That for better failure messages.

            if (!condition())
            {
                throw new Exception("Failed assertion!");
            }
        }
    }
}