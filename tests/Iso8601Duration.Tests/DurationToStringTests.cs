using Xunit;

namespace Iso8601Duration.Tests
{
    public class DurationToStringTests
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
        public void ToString_returns_original_string(string input)
        {
            var duration = Duration.Parse(input);
            var output = duration.ToString();
            Assert.Equal(input, output);
        }
    }
}
