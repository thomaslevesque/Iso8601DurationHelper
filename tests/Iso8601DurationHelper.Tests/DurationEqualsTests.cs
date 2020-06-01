using Xunit;

namespace Iso8601DurationHelper.Tests
{
    public class DurationEqualsTests
    {
        [Theory]
        [InlineData("P1Y")]
        [InlineData("P2M")]
        [InlineData("P3W")]
        [InlineData("P4D")]
        [InlineData("PT5H")]
        [InlineData("PT6M")]
        [InlineData("PT7S")]
        [InlineData("P1Y2M3W4DT5H6M7S")]
        public void Identical_duration_is_equal(string input)
        {
            var duration = Duration.Parse(input);
            Assert.Equal(duration, duration);
        }

        [Theory]
        [InlineData("P1Y")]
        [InlineData("P2M")]
        [InlineData("P3W")]
        [InlineData("P4D")]
        [InlineData("PT5H")]
        [InlineData("PT6M")]
        [InlineData("PT7S")]
        [InlineData("P1Y2M3W4DT5H6M7S")]
        public void Identical_duration_is_equal_with_operator(string input)
        {
            var duration1 = Duration.Parse(input);
            var duration2 = Duration.Parse(input);
            Assert.True(duration1 == duration2);
            Assert.False(duration1 != duration2);
        }

        [Theory]
        [InlineData("P1Y", "P2Y")]
        [InlineData("P1Y", "P1M")]
        [InlineData("P1Y2M3W4DT5H6M7S", "P1Y2M3W4DT5H6M9S")]
        [InlineData("P1Y2M3W4DT5H6M7S", "P1Y2M3W4DT5H9M7S")]
        [InlineData("P1Y2M3W4DT5H6M7S", "P1Y2M3W4DT9H6M7S")]
        [InlineData("P1Y2M3W4DT5H6M7S", "P1Y2M3W9DT5H6M7S")]
        [InlineData("P1Y2M3W4DT5H6M7S", "P1Y2M9W4DT5H6M7S")]
        [InlineData("P1Y2M3W4DT5H6M7S", "P1Y9M3W4DT5H6M7S")]
        [InlineData("P1Y2M3W4DT5H6M7S", "P9Y2M3W4DT5H6M7S")]
        public void Different_duration_is_not_equal(string input1, string input2)
        {
            var duration1 = Duration.Parse(input1);
            var duration2 = Duration.Parse(input2);
            Assert.NotEqual(duration1, duration2);
        }

        [Theory]
        [InlineData("P1Y", "P2Y")]
        [InlineData("P1Y", "P1M")]
        [InlineData("P1Y2M3W4DT5H6M7S", "P1Y2M3W4DT5H6M9S")]
        [InlineData("P1Y2M3W4DT5H6M7S", "P1Y2M3W4DT5H9M7S")]
        [InlineData("P1Y2M3W4DT5H6M7S", "P1Y2M3W4DT9H6M7S")]
        [InlineData("P1Y2M3W4DT5H6M7S", "P1Y2M3W9DT5H6M7S")]
        [InlineData("P1Y2M3W4DT5H6M7S", "P1Y2M9W4DT5H6M7S")]
        [InlineData("P1Y2M3W4DT5H6M7S", "P1Y9M3W4DT5H6M7S")]
        [InlineData("P1Y2M3W4DT5H6M7S", "P9Y2M3W4DT5H6M7S")]
        public void Different_duration_is_not_equal_with_operator(string input1, string input2)
        {
            var duration1 = Duration.Parse(input1);
            var duration2 = Duration.Parse(input2);
            Assert.False(duration1 == duration2);
            Assert.True(duration1 != duration2);
        }
    }
}
