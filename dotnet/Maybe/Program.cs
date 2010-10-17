using System;
using System.Diagnostics;

namespace Maybe
{
    class Program
    {
        private const int Iterations = 1000000;

        static void Main()
        {
            var customer = new Customer { Address = new Address { PostalCode = "92011" } };
            var customerWithNullPostalCode = new Customer { Address = new Address { PostalCode = null } };
            var customerWithEmptyPostalCode = new Customer { Address = new Address { PostalCode = "" } };
            var customerWithNullAddress = new Customer { Address = null };
            var nullCustomer = (Customer)null;

            PrintPostalCode(customer);
            PrintPostalCode(customerWithNullPostalCode);
            PrintPostalCode(customerWithEmptyPostalCode);
            PrintPostalCode(customerWithNullAddress);
            PrintPostalCode(nullCustomer);

            Time("Old", customer, Old);
            Time("New", customer, New);
            Time("Old (nullCustomer)", nullCustomer, Old);
            Time("New (nullCustomer)", nullCustomer, New);
        }

        private static void PrintPostalCode(Customer customer)
        {
            customer.Get(c => c.Address)
                    .Get(a => a.PostalCode)
                    .DefaultIf(string.IsNullOrEmpty, "none")
                    .Do(Console.WriteLine);
        }

        private static void Time(string name, Customer customer, Action<Customer> action)
        {
            var sw = Stopwatch.StartNew();

            for (int i = 0; i < Iterations; i++)
            {
                action(customer);
            }

            sw.Stop();

            Console.WriteLine("{0}: {1}ms", name, sw.ElapsedMilliseconds);
        }

        private static void Old(Customer customer)
        {
            string postalCode;

            if (customer != null && customer.Address != null && string.IsNullOrEmpty(customer.Address.PostalCode))
            {
                postalCode = customer.Address.PostalCode;
            }
            else
            {
                postalCode = "none";
            }
        }

        private static void New(Customer customer)
        {
            var postalCode = customer.Get(c => c.Address)
                                     .Get(a => a.PostalCode)
                                     .DefaultIf(string.IsNullOrEmpty, "none");
        }
    }

    class Customer { public Address Address { get; set; } }

    class Address { public string PostalCode { get; set; } }

    public static class MaybeExtensions
    {
        public static TOut Get<TIn, TOut>(this TIn value, Func<TIn, TOut> selector)
            where TIn : class
        {
            if (value == null)
                return default(TOut);

            return selector(value);
        }

        public static T If<T>(this T value, Func<T, bool> predicate)
            where T : class
        {
            if (value != null && predicate(value))
                return value;

            return null;
        }

        public static T Unless<T>(this T value, Func<T, bool> predicate)
            where T : class
        {
            if (value == null || predicate(value))
                return null;

            return value;
        }

        public static T Default<T>(this T value, T defaultValue)
            where T : class
        {
            if (value == null)
                return defaultValue;

            return value;
        }

        public static T DefaultIf<T>(this T value, Func<T, bool> predicate, T defaultValue)
            where T : class
        {
            if (value == null || predicate(value))
                return defaultValue;

            return value;
        }

        public static T DefaultUnless<T>(this T value, Func<T, bool> predicate, T defaultValue)
            where T : class
        {
            if (value != null && predicate(value))
                return value;

            return defaultValue;
        }

        public static T Do<T>(this T value, Action<T> action)
            where T : class
        {
            if (value != null)
                action(value);

            return value;
        }

        public static T Else<T>(this T value, Action action)
            where T : class
        {
            if (value == null)
                action();

            return value;
        }
    }
}