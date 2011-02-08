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
                .Add(1)
                .Add(2)
                .Add(3)
                .Build();

            Assert.AreEqual("1, 2, 3", value);
        }

        [Test]
        public void It_accepts_a_custom_separator()
        {
            var builder = new DelimitedStringBuilder<int>("|");

            var value = builder
                .Add(1)
                .Add(2)
                .Add(3)
                .Build();

            Assert.AreEqual("1|2|3", value);
        }

        [Test]
        public void It_uses_ToString_as_the_default_formatter()
        {
            var builder = new DelimitedStringBuilder<MyObject>();

            var value = builder
                .Add(new MyObject(1))
                .Add(new MyObject(2))
                .Add(new MyObject(3))
                .Build();

            Assert.AreEqual("*1*, *2*, *3*", value);
        }

        [Test]
        public void It_accepts_a_custom_formatter()
        {
            var builder = new DelimitedStringBuilder<MyObject>
                          {
                              Formatter = i => string.Format("_{0}_", i.Value)
                          };

            var value = builder
                .Add(new MyObject(1))
                .Add(new MyObject(2))
                .Add(new MyObject(3))
                .Build();

            Assert.AreEqual("_1_, _2_, _3_", value);
        }

        [Test]
        public void It_defers_using_the_formatter_until_the_Build_method_is_invoked()
        {
            var builder = new DelimitedStringBuilder<MyObject>();

            var item1 = new MyObject(1);
            var item2 = new MyObject(2);
            var item3 = new MyObject(3);

            builder
                .Add(item1)
                .Add(item2)
                .Add(item3);

            item1.Value = 11;
            item2.Value = 22;
            item3.Value = 33;

            var value = builder.Build();

            Assert.AreEqual("*11*, *22*, *33*", value);
        }

        [Test]
        public void It_accepts_a_prefix_and_suffix_for_the_whole_string()
        {
            var builder = new DelimitedStringBuilder<int>
                          {
                              Prefix = "{ ",
                              Suffix = " }"
                          };

            var value = builder
                .Add(1)
                .Add(2)
                .Add(3)
                .Build();

            Assert.AreEqual("{ 1, 2, 3 }", value);
        }

        [Test]
        public void It_accepts_a_prefix_and_suffix_for_each_item()
        {
            var builder = new DelimitedStringBuilder<int>
                          {
                              ItemPrefix = "(",
                              ItemSuffix = ")"
                          };

            var value = builder
                .Add(1)
                .Add(2)
                .Add(3)
                .Build();

            Assert.AreEqual("(1), (2), (3)", value);
        }

        [Test]
        public void It_returns_the_empty_string_when_no_items_are_added()
        {
            var builder = new DelimitedStringBuilder<int>();

            var value = builder
                .Build();

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
                .Build();

            Assert.AreEqual("-empty-", value);
        }

        [Test]
        public void It_can_be_used_to_create_SQL_statements()
        {
            var andBuilder = new DelimitedStringBuilder<string>
                             {
                                 Separator = " AND ",
                                 ItemPrefix = "(",
                                 ItemSuffix = ")",
                             };

            var orBuilder = new DelimitedStringBuilder<string>
                             {
                                 Separator = " OR ",
                                 ItemPrefix = "(",
                                 ItemSuffix = ")",
                             };

            orBuilder.Add("c = 3").Add("d = 4");

            var whereClause = andBuilder
                .Add("a = 1")
                .Add("b = 2")
                .Add(orBuilder.Build())
                .Build();

            Assert.AreEqual("(a = 1) AND (b = 2) AND ((c = 3) OR (d = 4))", whereClause);
        }
    }

    public class MyObject
    {
        public MyObject(int value)
        {
            Value = value;
        }

        public int Value { get; set; }

        public override string ToString()
        {
            return string.Format("*{0}*", Value);
        }
    }
}