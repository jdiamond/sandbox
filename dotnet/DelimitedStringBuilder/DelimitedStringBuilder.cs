using System;
using System.Collections.Generic;
using System.Text;

namespace DelimitedStringBuilder
{
    public class DelimitedStringBuilder<T>
    {
        private readonly StringBuilder _builder = new StringBuilder();

        public DelimitedStringBuilder()
        {
            Separator = ", ";
        }

        /// <summary>
        /// Header for the whole string, if there are any items.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Prefix to appear before each item.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Delegate to format items as strings. When T is a reference type, should handle nulls.
        /// </summary>
        public Func<T, string> Formatter { get; set; }

        /// <summary>
        /// Separator between each item.
        /// </summary>
        public string Separator { get; set; }

        /// <summary>
        /// Suffix to appear after each item.
        /// </summary>
        public string Suffix { get; set; }

        /// <summary>
        /// Footer for the whole string, if there are any items.
        /// </summary>
        public string Footer { get; set; }

        /// <summary>
        /// Value to return if no items are added.
        /// </summary>
        public string EmptyValue { get; set; }

        public DelimitedStringBuilder<T> Append(T item)
        {
            AppendCore(item, _builder);

            return this;
        }

        private void AppendCore(T item, StringBuilder builder)
        {
            var value = Format(item);

            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            if (builder.Length > 0)
            {
                builder.Append(Separator);
            }

            if (Prefix != null)
            {
                builder.Append(Prefix);
            }

            builder.Append(value);

            if (Suffix != null)
            {
                builder.Append(Suffix);
            }
        }

        private string Format(T item)
        {
            return Formatter != null ? Formatter(item) : string.Format("{0}", item);
        }

        public DelimitedStringBuilder<T> Clear()
        {
            _builder.Length = 0;

            return this;
        }

        public override string ToString()
        {
            return ToStringCore(_builder);
        }

        public string ToString(IEnumerable<T> items)
        {
            var builder = new StringBuilder();
            
            foreach (var item in items)
            {
                AppendCore(item, builder);
            }

            return ToStringCore(builder);
        }

        private string ToStringCore(StringBuilder builder)
        {
            if (builder.Length == 0)
            {
                return EmptyValue ?? "";
            }

            return (Header ?? "") + builder + (Footer ?? "");
        }
    }
}