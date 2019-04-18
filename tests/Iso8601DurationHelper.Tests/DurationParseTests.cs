using System;
using Xunit;

namespace Iso8601DurationHelper.Tests
{
    public class DurationParseTests
    {
        [Theory]
        [InlineData( "P1Y", 1, 0, 0, 0, 0, 0, 0)]
        [InlineData( "P2M", 0, 2, 0, 0, 0, 0, 0)]
        [InlineData( "P3W", 0, 0, 3, 0, 0, 0, 0)]
        [InlineData( "P4D", 0, 0, 0, 4, 0, 0, 0)]
        [InlineData("PT5H", 0, 0, 0, 0, 5, 0, 0)]
        [InlineData("PT6M", 0, 0, 0, 0, 0, 6, 0)]
        [InlineData("PT7S", 0, 0, 0, 0, 0, 0, 7)]
        [InlineData("P1Y2M3W4DT5H6M7S", 1, 2, 3, 4, 5, 6, 7)]
        public void Valid_ISO8601_duration_is_parsed_correctly(string input, uint years, uint months, uint weeks, uint days, uint hours, uint minutes, uint seconds)
        {
            var duration = Duration.Parse(input);
            Assert.Equal(years, duration.Years);
            Assert.Equal(months, duration.Months);
            Assert.Equal(weeks, duration.Weeks);
            Assert.Equal(days, duration.Days);
            Assert.Equal(hours, duration.Hours);
            Assert.Equal(minutes, duration.Minutes);
            Assert.Equal(seconds, duration.Seconds);
        }

        [Theory]
        [InlineData("")]
        [InlineData("P")]
        [InlineData("P1H")] // Time in date part
        [InlineData("PT1D")] // Date in time part
        [InlineData("P1M2Y")] // Components in wrong order
        [InlineData("PT1M2H")] // Components in wrong order
        [InlineData("P1Z")] // Invalid component
        [InlineData("P1Y---2M")] // Invalid characters after component
        [InlineData("P1Y2M+++")] // Trailing invalid characters
        public void Invalid_ISO8601_duration_throws_exception(string input)
        {
            Assert.Throws<FormatException>(() => Duration.Parse(input));
        }
    }
}
