using Xunit;

namespace Iso8601DurationHelper.Tests
{
    public class DurationFromTests
    {
        [Theory]
        [InlineData(1U)]
        [InlineData(42U)]
        [InlineData(123U)]
        public void FromYears_returns_correct_duration(uint years)
        {
            var duration = Duration.FromYears(years);
            Assert.Equal(years, duration.Years);
            Assert.Equal(0U, duration.Months);
            Assert.Equal(0U, duration.Weeks);
            Assert.Equal(0U, duration.Days);
            Assert.Equal(0U, duration.Hours);
            Assert.Equal(0U, duration.Minutes);
            Assert.Equal(0U, duration.Seconds);
        }

        [Theory]
        [InlineData(1U)]
        [InlineData(42U)]
        [InlineData(123U)]
        public void FromMonths_returns_correct_duration(uint months)
        {
            var duration = Duration.FromMonths(months);
            Assert.Equal(0U, duration.Years);
            Assert.Equal(months, duration.Months);
            Assert.Equal(0U, duration.Weeks);
            Assert.Equal(0U, duration.Days);
            Assert.Equal(0U, duration.Hours);
            Assert.Equal(0U, duration.Minutes);
            Assert.Equal(0U, duration.Seconds);
        }

        [Theory]
        [InlineData(1U)]
        [InlineData(42U)]
        [InlineData(123U)]
        public void FromWeeks_returns_correct_duration(uint weeks)
        {
            var duration = Duration.FromWeeks(weeks);
            Assert.Equal(0U, duration.Years);
            Assert.Equal(0U, duration.Months);
            Assert.Equal(weeks, duration.Weeks);
            Assert.Equal(0U, duration.Days);
            Assert.Equal(0U, duration.Hours);
            Assert.Equal(0U, duration.Minutes);
            Assert.Equal(0U, duration.Seconds);
        }

        [Theory]
        [InlineData(1U)]
        [InlineData(42U)]
        [InlineData(123U)]
        public void FromDays_returns_correct_duration(uint days)
        {
            var duration = Duration.FromDays(days);
            Assert.Equal(0U, duration.Years);
            Assert.Equal(0U, duration.Months);
            Assert.Equal(0U, duration.Weeks);
            Assert.Equal(days, duration.Days);
            Assert.Equal(0U, duration.Hours);
            Assert.Equal(0U, duration.Minutes);
            Assert.Equal(0U, duration.Seconds);
        }

        [Theory]
        [InlineData(1U)]
        [InlineData(42U)]
        [InlineData(123U)]
        public void FromHours_returns_correct_duration(uint hours)
        {
            var duration = Duration.FromHours(hours);
            Assert.Equal(0U, duration.Years);
            Assert.Equal(0U, duration.Months);
            Assert.Equal(0U, duration.Weeks);
            Assert.Equal(0U, duration.Days);
            Assert.Equal(hours, duration.Hours);
            Assert.Equal(0U, duration.Minutes);
            Assert.Equal(0U, duration.Seconds);
        }

        [Theory]
        [InlineData(1U)]
        [InlineData(42U)]
        [InlineData(123U)]
        public void FromMinutes_returns_correct_duration(uint minutes)
        {
            var duration = Duration.FromMinutes(minutes);
            Assert.Equal(0U, duration.Years);
            Assert.Equal(0U, duration.Months);
            Assert.Equal(0U, duration.Weeks);
            Assert.Equal(0U, duration.Days);
            Assert.Equal(0U, duration.Hours);
            Assert.Equal(minutes, duration.Minutes);
            Assert.Equal(0U, duration.Seconds);
        }

        [Theory]
        [InlineData(1U)]
        [InlineData(42U)]
        [InlineData(123U)]
        public void FromSeconds_returns_correct_duration(uint seconds)
        {
            var duration = Duration.FromSeconds(seconds);
            Assert.Equal(0U, duration.Years);
            Assert.Equal(0U, duration.Months);
            Assert.Equal(0U, duration.Weeks);
            Assert.Equal(0U, duration.Days);
            Assert.Equal(0U, duration.Hours);
            Assert.Equal(0U, duration.Minutes);
            Assert.Equal(seconds, duration.Seconds);
        }
    }
}