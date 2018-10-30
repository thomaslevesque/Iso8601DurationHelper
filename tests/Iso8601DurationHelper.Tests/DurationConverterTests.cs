using System;
using System.ComponentModel;
using Xunit;

namespace Iso8601DurationHelper.Tests
{
    public class DurationConverterTests
    {
        [Fact]
        public void DurationConverter_is_converter_for_Duration()
        {
            var converter = TypeDescriptor.GetConverter(typeof(Duration));
            Assert.IsType<DurationConverter>(converter);
        }

        [Fact]
        public void Can_convert_from_string()
        {
            var converter = new DurationConverter();
            Assert.True(converter.CanConvertFrom(typeof(string)));
        }

        [Theory]
        [InlineData(typeof(bool))]
        [InlineData(typeof(object))]
        public void Cannot_convert_from_other_type(Type type)
        {
            var converter = new DurationConverter();
            Assert.False(converter.CanConvertFrom(type));
        }

        [Fact]
        public void Can_convert_to_string()
        {
            var converter = new DurationConverter();
            Assert.True(converter.CanConvertTo(typeof(string)));
        }

        [Theory]
        [InlineData(typeof(bool))]
        [InlineData(typeof(object))]
        public void Cannot_convert_to_other_type(Type type)
        {
            var converter = new DurationConverter();
            Assert.False(converter.CanConvertTo(type));
        }

        [Theory]
        [InlineData( "P1Y", 1, 0, 0, 0, 0, 0, 0)]
        [InlineData( "P2M", 0, 2, 0, 0, 0, 0, 0)]
        [InlineData( "P3W", 0, 0, 3, 0, 0, 0, 0)]
        [InlineData( "P4D", 0, 0, 0, 4, 0, 0, 0)]
        [InlineData("PT5H", 0, 0, 0, 0, 5, 0, 0)]
        [InlineData("PT6M", 0, 0, 0, 0, 0, 6, 0)]
        [InlineData("PT7S", 0, 0, 0, 0, 0, 0, 7)]
        [InlineData("P1Y2M3W4DT5H6M7S", 1, 2, 3, 4, 5, 6, 7)]
        public void Can_convert_from_valid_Iso8601_string(string input, uint years, uint months, uint weeks, uint days, uint hours, uint minutes, uint seconds)
        {
            var converter = new DurationConverter();
            var duration = (Duration)converter.ConvertFrom(input);
            Assert.Equal(years, duration.Years);
            Assert.Equal(months, duration.Months);
            Assert.Equal(weeks, duration.Weeks);
            Assert.Equal(days, duration.Days);
            Assert.Equal(hours, duration.Hours);
            Assert.Equal(minutes, duration.Minutes);
            Assert.Equal(seconds, duration.Seconds);
        }
    }
}