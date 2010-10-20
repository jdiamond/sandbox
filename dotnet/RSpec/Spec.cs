using System;
using System.Collections.Generic;

namespace RSpec
{
    public class Spec
    {
        private static Action _beforeEach;
        private static ExampleGroup _currentExampleGroup;
        private static ExampleGroup _previousExampleGroup;
        private static readonly List<ExampleGroup> _exampleGroups = new List<ExampleGroup>();

        protected static void BeforeEach(Action block)
        {
            _beforeEach = block;
        }

        protected static void Describe(string subject, Action block)
        {
            var newExampleGroup = new ExampleGroup(subject)
                                      {
                                          Parent = _currentExampleGroup,
                                          BeforeEach = _beforeEach
                                      };

            _currentExampleGroup = newExampleGroup;
            _beforeEach = null;

            if (_currentExampleGroup.Parent != null)
            {
                _currentExampleGroup.Parent.Children.Add(_currentExampleGroup);
            }
            else
            {
                _exampleGroups.Add(_currentExampleGroup);
            }

            block();

            _previousExampleGroup = _currentExampleGroup;
            _currentExampleGroup = _currentExampleGroup.Parent;
        }

        protected static void It(string description)
        {
            It(description, () => { throw new NotImplementedException(); });
        }

        protected static void It(string description, Action block)
        {
            if (_currentExampleGroup == null)
            {
                throw new Exception("You need to use Describe first!");
            }

            _currentExampleGroup.Examples.Add(new Example(description, block));
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

            reporter.RunStarted();

            foreach (var exampleGroup in _exampleGroups)
            {
                exampleGroup.Run(reporter);
            }

            reporter.RunFinished();
        }
    }
}