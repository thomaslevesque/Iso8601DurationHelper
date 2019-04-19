using System;
using System.ComponentModel;
using System.Globalization;

namespace Iso8601DurationHelper
{
    /// <summary>
    /// Provides a type converter to convert <see cref="Duration"/> objects to and from other representations.
    /// </summary>
    public class DurationConverter : TypeConverter
    {
        /// <summary>
        /// Gets a value indicating whether this converter can convert an object in the given source type to a <see cref="Duration"/> using the specified context.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="sourceType">A <see cref="Type"/> that represents the type you wish to convert from.</param>
        /// <returns><c>true</c> if this converter can perform the conversion; otherwise, <c>false</c>.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// Gets a value indicating whether this converter can convert an object to the given destination type using the context.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="destinationType">A <see cref="Type"/> that represents the type you wish to convert to.</param>
        /// <returns><c>true</c> if this converter can perform the conversion; otherwise, <c>false</c>.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Converts the given object to a <see cref="Duration"/>.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="culture">An optional <see cref="CultureInfo"/>. If not supplied, the current culture is assumed.</param>
        /// <param name="value">The <see cref="Object"/> to convert.</param>
        /// <returns>An <see cref="Object"/> that represents the converted value.</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string s)
            {
                return Duration.Parse(s);
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Converts the given object to another type.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="culture">The culture into which value will be converted.</param>
        /// <param name="value">The object to convert.</param>
        /// <param name="destinationType">The type to convert the object to.</param>
        /// <returns>The converted object.</returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((Duration)value).ToString();
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
