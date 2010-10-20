using System;

namespace RSpec
{
    public class Describe_Calculator : Spec
    {
        private Calculator calculator;

        public void BeforeEach()
        {
            calculator = new Calculator();
        }

        public void It_can_add_two_numbers()
        {
            int sum = calculator.Add(1, 2);
            Assert(() => sum == 3);
        }

        public void It_can_subtract_two_numbers()
        {
            int difference = calculator.Subtract(1, 2);
            Assert(() => difference == 1); // Fail!
        }

        public void It_can_divide_two_numbers()
        {
            throw new NotImplementedException();
        }
    }
}