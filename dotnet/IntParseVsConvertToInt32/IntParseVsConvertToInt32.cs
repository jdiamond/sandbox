using System;
using System.Globalization;
using System.Threading;
using NUnit.Framework;

namespace IntParseVsConvertToInt32
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void SetUp()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        }

        [Test]
        public void IntParse()
        {
            Assert.AreEqual(123, int.Parse("123"));
        }

        [Test]
        public void ConvertToInt32()
        {
            Assert.AreEqual(123, Convert.ToInt32("123"));
        }

        [Test, Category("Behavior Difference")]
        public void Null_IntParse_Fails()
        {
            Assert.Throws<ArgumentNullException>(() => int.Parse(null));
        }

        [Test, Category("Behavior Difference")]
        public void Null_ConvertToInt32()
        {
            Assert.AreEqual(0, Convert.ToInt32(null));
        }

        [Test]
        public void EmptyString_IntParse_Fails()
        {
            Assert.Throws<FormatException>(() => int.Parse(""));
        }

        [Test]
        public void EmptyString_ConvertToInt32_Fails()
        {
            Assert.Throws<FormatException>(() => Convert.ToInt32(""));
        }

        [Test]
        public void DecimalPoint_IntParse_Fails()
        {
            Assert.Throws<FormatException>(() => int.Parse("123.45"));
        }

        [Test]
        public void DecimalPoint_ConvertToInt32_Fails()
        {
            Assert.Throws<FormatException>(() => Convert.ToInt32("123.45"));
        }

        [Test]
        public void Comma_IntParse_Fails()
        {
            Assert.Throws<FormatException>(() => int.Parse("123,45"));
        }

        [Test]
        public void Comma_ConvertToInt32_Fails()
        {
            Assert.Throws<FormatException>(() => Convert.ToInt32("123,45"));
        }

        [Test]
        public void SpaceBefore_IntParse()
        {
            Assert.AreEqual(123, int.Parse(" 123"));
        }

        [Test]
        public void SpaceBefore_ConvertToInt32()
        {
            Assert.AreEqual(123, Convert.ToInt32(" 123"));
        }

        [Test]
        public void SpaceAfter_IntParse()
        {
            Assert.AreEqual(123, int.Parse("123 "));
        }

        [Test]
        public void SpaceAfter_ConvertToInt32()
        {
            Assert.AreEqual(123, Convert.ToInt32("123 "));
        }

        [Test]
        public void SpaceInside_IntParse_Fails()
        {
            Assert.Throws<FormatException>(() => int.Parse("1 23"));
        }

        [Test]
        public void SpaceInside_ConvertToInt32_Fails()
        {
            Assert.Throws<FormatException>(() => Convert.ToInt32("1 23"));
        }

        [Test, Category("API Difference")]
        public void HexNumber_IntParse()
        {
            Assert.AreEqual(0xABC, int.Parse("ABC", NumberStyles.HexNumber));
        }

        [Test, Category("API Difference")]
        public void HexNumber_ConvertToInt32()
        {
            Assert.AreEqual(0xABC, Convert.ToInt32("ABC", 16));
        }

        [Test, Category("API Difference")]
        public void NonStrings_IntParse()
        {
            //Assert.AreEqual(123, int.Parse(123m)); // Won't compile!
        }

        [Test, Category("API Difference")]
        public void NonStrings_ConvertToInt32()
        {
            Assert.AreEqual(123, Convert.ToInt32(123m));
        }
    }
}