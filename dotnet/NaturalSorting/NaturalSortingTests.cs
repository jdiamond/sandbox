using System.Linq;
using NUnit.Framework;

namespace NaturalSorting
{
    [TestFixture]
    public class NaturalSortingTests
    {
        [Test]
        public void NaturalSortingCanBeDoneByPaddingAllTheNumbers()
        {
            var unsorted = new[]
                         {
                             "10.10.10",
                             "9.9.9"
                         };

            var sorted = unsorted.OrderBy(s => s.PadNumbers(2));

            CollectionAssert.AreEqual(new[]
                                      {
                                          "9.9.9",
                                          "10.10.10"
                                      },
                                      sorted.ToArray());
        }
    }
}