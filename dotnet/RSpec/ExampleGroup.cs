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
            Children = new List<ExampleGroup>();
        }

        public string Subject { get; set; }
        public Action BeforeEach { get; set; }

        public List<Example> Examples { get; set; }
        public ExampleGroup Parent { get; set; }
        public List<ExampleGroup> Children { get; set; }

        public void Run(Reporter reporter)
        {
            reporter.ExampleGroupStarted(this);

            foreach (var example in Examples)
            {
                if (BeforeEach != null)
                {
                    BeforeEach();
                }

                example.Run(reporter);
            }

            foreach (var child in Children)
            {
                if (BeforeEach != null)
                {
                    BeforeEach();
                }

                child.Run(reporter);
            }
        }
    }
}