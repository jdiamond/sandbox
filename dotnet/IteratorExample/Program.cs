using System;
using System.Collections.Generic;
using System.Linq;

namespace IteratorExample
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Calling GetInts() with arguments that should make it throw...");

            var ints = GetInts(10, 0);

            Console.WriteLine("No exception thrown yet.");

            Console.WriteLine("Let's try calling GetEnumerator()...");

            var e = ints.GetEnumerator();

            Console.WriteLine("Nope, that wasn't it.");

            try
            {
                Console.WriteLine("How about MoveNext()?");

                e.MoveNext();

                Console.WriteLine("You'll never see this!");
            }
            catch
            {
                Console.WriteLine("It was MoveNext() that threw!");
            }

            Console.WriteLine("BetterGetInts() isn't actually an iterator so it throws right away...");

            try
            {
                ints = BetterGetInts(10, 0);

                Console.WriteLine("You'll never see this!");
            }
            catch
            {
                Console.WriteLine("See?");
            }

            Console.WriteLine("Calling BetterGetInts() so that it doesn't immediately throw...");

            ints = BetterGetInts(99, 101);

            Console.WriteLine("Evaluating the iterator with ToArray or foreach makes it throw!");

            try
            {
                var array = ints.ToArray();

                Console.WriteLine("Once again, you'll never see this!");
            }
            catch
            {
                Console.WriteLine("This makes sense but is easy to forget.");
            }
        }

        static IEnumerable<int> GetInts(int from, int to)
        {
            if (from > to)
                throw new ArgumentException("From must be less than to!");

            for (int i = from; i < to; i++)
            {
                yield return i;
            }
        }

        static IEnumerable<int> BetterGetInts(int from, int to)
        {
            if (from > to)
                throw new ArgumentException("from must be less than to");

            return BetterGetIntsHelper(from, to);
        }

        static IEnumerable<int> BetterGetIntsHelper(int from, int to)
        {
            for (int i = from; i < to; i++)
            {
                if (i >= 100)
                {
                    throw new Exception("I can't count that high!");
                }

                yield return i;
            }
        }
    }
}