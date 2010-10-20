using System;
using System.Collections.Generic;

namespace RSpec
{
    public class ExampleGroup
    {
        public ExampleGroup(string subject)
        {
            Subject = subject;
            Examples = new List<Example>();
        }

        public string Subject { get; set; }
        public Action BeforeEach { get; set; }

        public List<Example> Examples { get; set; }

        public void Run(Reporter reporter)
        {
            reporter.Start(this);

            foreach (var example in Examples)
            {
                if (BeforeEach != null)
                {
                    BeforeEach();
                }

                example.Run(reporter);
            }
        }
    }
}