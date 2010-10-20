namespace RSpec
{
    public class Program : Spec
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
                    Assert(() => difference == 1);
                });

                It("can divide two numbers");
            });

            Run();
        }
    }
}