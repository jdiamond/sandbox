using System;

namespace RSpec
{
    class Program
    {
        static void Main()
        {
            Calculator calculator = null;

            BeforeEach(() =>
                           {
                               calculator = new Calculator();
                           });

            Describe("Calculator", () =>
            {
                It("can add two numbers", () =>
                {
                    int sum = calculator.Add(1, 2);
                    Assert(() => sum == 3);
                });

                It("can subtract two numbers", () =>
                {
                    int difference = calculator.Subtract(1, 2);
                    Assert(() => difference == -1);
                });
            });
        }

        static Action _beforeEach;

        static void BeforeEach(Action action)
        {
            _beforeEach = action;
        }

        static void Describe(string subject, Action action)
        {
            Console.WriteLine(subject);
            action();
        }

        static void It(string description, Action action)
        {
            Console.WriteLine("  {0}", description);
            if (_beforeEach != null) _beforeEach();
            action();
        }

        static void Assert(Func<bool> condition)
        {
            if (!condition())
                throw new Exception("Assertion failed!");
        }
    }

    public class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Subtract(int a, int b)
        {
            return a - b;
        }
    }
}