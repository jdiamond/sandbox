using System.Collections.Generic;
using NUnit.Framework;

namespace DelimitedStringBuilder
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void It_uses_a_comma_as_the_default_separator()
        {
            var builder = new DelimitedStringBuilder<int>();

            var value = builder
                .Append(1)
                .Append(2)
                .Append(3)
                .ToString();

            Assert.AreEqual("1, 2, 3", value);
        }

        [Test]
        public void It_accepts_a_custom_separator()
        {
            var builder = new DelimitedStringBuilder<int>
                              {
                                  Separator = "|"
                              };

            var value = builder
                .Append(1)
                .Append(2)
                .Append(3)
                .ToString();

            Assert.AreEqual("1|2|3", value);
        }

        [Test]
        public void It_uses_ToString_as_the_default_formatter()
        {
            var builder = new DelimitedStringBuilder<object>();

            var value = builder
                .Append(new { x = 1 })
                .Append(new { x = 2 })
                .Append(new { x = 3 })
                .ToString();

            Assert.AreEqual("{ x = 1 }, { x = 2 }, { x = 3 }", value);
        }

        [Test]
        public void It_accepts_a_custom_formatter()
        {
            var builder = new DelimitedStringBuilder<int>
                          {
                              Formatter = i => i.ToString("x")
                          };

            var value = builder
                .Append(1)
                .Append(127)
                .Append(255)
                .ToString();

            Assert.AreEqual("1, 7f, ff", value);
        }

        [Test]
        public void It_accepts_a_header_and_a_prefix_for_the_whole_string()
        {
            var builder = new DelimitedStringBuilder<int>
                          {
                              Header = "{ ",
                              Footer = " }"
                          };

            var value = builder
                .Append(1)
                .Append(2)
                .Append(3)
                .ToString();

            Assert.AreEqual("{ 1, 2, 3 }", value);
        }

        [Test]
        public void It_accepts_a_prefix_and_suffix_for_each_item()
        {
            var builder = new DelimitedStringBuilder<int>
                          {
                              Prefix = "(",
                              Suffix = ")"
                          };

            var value = builder
                .Append(1)
                .Append(2)
                .Append(3)
                .ToString();

            Assert.AreEqual("(1), (2), (3)", value);
        }

        [Test]
        public void It_returns_the_empty_string_when_no_items_are_added()
        {
            var builder = new DelimitedStringBuilder<int>();

            var value = builder
                .ToString();

            Assert.AreEqual("", value);
        }

        [Test]
        public void It_accepts_a_custom_empty_value()
        {
            var builder = new DelimitedStringBuilder<int>
            {
                EmptyValue = "-empty-"
            };

            var value = builder
                .ToString();

            Assert.AreEqual("-empty-", value);
        }

        [Test]
        public void It_skips_null_items()
        {
            var builder = new DelimitedStringBuilder<string>();

            var value = builder
                .Append("a")
                .Append(null)
                .Append("b")
                .ToString();

            Assert.AreEqual("a, b", value);
        }

        [Test]
        public void It_skips_items_that_format_as_the_empty_string()
        {
            var builder = new DelimitedStringBuilder<string>();

            var value = builder
                .Append("a")
                .Append("")
                .Append("b")
                .ToString();

            Assert.AreEqual("a, b", value);
        }

        [Test]
        public void It_has_a_Clear_method_that_clears_the_builder()
        {
            var builder = new DelimitedStringBuilder<int>();

            var value = builder
                .Append(1)
                .Append(2)
                .Append(3)
                .Clear()
                .Append(4)
                .Append(5)
                .Append(6)
                .ToString();

            Assert.AreEqual("4, 5, 6", value);
        }

        [Test]
        public void It_has_a_ToString_overload_that_uses_the_specified_items()
        {
            var builder = new DelimitedStringBuilder<int>
                              {
                                  Separator = "|"
                              };

            var value = builder.ToString(new List<int> { 1, 2, 3 });

            Assert.AreEqual("1|2|3", value);
        }

        [Test]
        public void It_can_be_used_to_create_SQL_statements()
        {
            var andBuilder = new DelimitedStringBuilder<object>
            {
                Separator = " AND ",
                Prefix = "(",
                Suffix = ")",
            };

            var orBuilder = new DelimitedStringBuilder<string>
            {
                Separator = " OR ",
                Prefix = "(",
                Suffix = ")",
            };

            andBuilder.Append("a = 1").Append("b = 2");
            orBuilder.Append("c = 3").Append("d = 4");
            andBuilder.Append(orBuilder);

            var whereClause = andBuilder.ToString();

            Assert.AreEqual("(a = 1) AND (b = 2) AND ((c = 3) OR (d = 4))", whereClause);
        }

        [Test]
        public void It_skips_nested_builders_that_are_empty()
        {
            var andBuilder = new DelimitedStringBuilder<object>
            {
                Separator = " AND ",
                Prefix = "(",
                Suffix = ")",
            };

            var orBuilder = new DelimitedStringBuilder<string>
            {
                Separator = " OR ",
                Prefix = "(",
                Suffix = ")",
            };

            andBuilder.Append("a = 1").Append("b = 2");
            andBuilder.Append(orBuilder);

            var whereClause = andBuilder.ToString();

            Assert.AreEqual("(a = 1) AND (b = 2)", whereClause);
        }
    }
}