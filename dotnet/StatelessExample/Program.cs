using System;
using Stateless;

namespace StatelessExample
{
    class Program
    {
        static void Main()
        {
            var sm = new StateMachine<State, Trigger>(State.A);

            sm.Configure(State.A)
              .Permit(Trigger.GoToB, State.B)
              .OnEntry(OnEnterA); // This never gets called.

            sm.Configure(State.B)
              .OnEntry(OnEnterB)
              .Permit(Trigger.GoToC, State.C);

            var goToCTrigger = sm.SetTriggerParameters<string>(Trigger.GoToC);

            sm.Configure(State.C)
              .OnEntryFrom(goToCTrigger, OnEnterC);

            Console.WriteLine(sm);
            sm.Fire(Trigger.GoToB);
            Console.WriteLine(sm);
            sm.Fire(goToCTrigger, "foo"); // Using Trigger.GoToC will throw an exception.
            Console.WriteLine(sm);
        }

        private static void OnEnterA()
        {
            Console.WriteLine("Entering state A.");
        }

        private static void OnEnterB()
        {
            Console.WriteLine("Entering state B.");
        }

        private static void OnEnterC(string arg)
        {
            Console.WriteLine("Entering state C with arg {0}.", arg);
        }
    }

    public enum State
    {
        A,
        B,
        C
    }

    public enum Trigger
    {
        GoToB,
        GoToC
    }
}