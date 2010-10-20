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
}