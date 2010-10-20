using System;

namespace RSpec
{
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
            reporter.ExampleStarted(this);

            try
            {
                Block();

                reporter.ExamplePassed(this);
            }
            catch (NotImplementedException)
            {
                reporter.ExamplePending(this);
            }
            catch (Exception)
            {
                reporter.ExampleFailed(this);
            }
        }
    }
}