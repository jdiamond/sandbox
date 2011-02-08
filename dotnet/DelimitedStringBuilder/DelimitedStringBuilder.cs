using System;
using System.Collections.Generic;
using System.Text;

namespace DelimitedStringBuilder
{
    public class DelimitedStringBuilder<T>
    {
        private readonly List<T> _items = new List<T>();

        public DelimitedStringBuilder()
            : this(", ", DefaultFormatter)
        {
        }

        public DelimitedStringBuilder(string separator)
            : this(separator, DefaultFormatter)
        {
        }

        public DelimitedStringBuilder(string separator, Func<T, string> formatter)
        {
            Separator = separator;
            Formatter = formatter;
        }

        public string Separator { get; set; }
        public Func<T, string> Formatter { get; set;  }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public string ItemPrefix { get; set; }
        public string ItemSuffix { get; set; }
        public string EmptyValue { get; set; }

        public DelimitedStringBuilder<T> Add(T item)
        {
            _items.Add(item);

            return this;
        }

        public string Build()
        {
            if (_items.Count == 0)
            {
                return EmptyValue ?? "";
            }

            var stringBuilder = new StringBuilder();

            if (Prefix != null)
            {
                stringBuilder.Append(Prefix);
            }

            bool needsSeparator = false;

            foreach (var item in _items)
            {
                if (needsSeparator)
                {
                    stringBuilder.Append(Separator);
                }

                if (ItemPrefix != null)
                {
                    stringBuilder.Append(ItemPrefix);
                }

                stringBuilder.Append(Formatter(item));

                if (ItemSuffix != null)
                {
                    stringBuilder.Append(ItemSuffix);
                }

                needsSeparator = true;
            }

            if (Suffix != null)
            {
                stringBuilder.Append(Suffix);
            }

            return stringBuilder.ToString();
        }

        private static string DefaultFormatter(T item)
        {
            return item.ToString();
        }
    }
}