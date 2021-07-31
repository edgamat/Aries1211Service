using Xunit;

namespace UnitTests.Aries1211.Domain
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData(" ", true)]
        [InlineData("a", false)]
        public void IsMissing(string input, bool expected)
        {
            Assert.Equal(expected, input.IsMissing());
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData(" ", false)]
        [InlineData("a", true)]
        public void IsNotMissing(string input, bool expected)
        {
            Assert.Equal(expected, input.IsNotMissing());
        }

        [Theory]
        [InlineData(null, 1, null)]
        [InlineData("", 1, "")]
        [InlineData("1234567890", 5, "12345")]
        public void TruncateTests(string input, int len, string expected)
        {
            Assert.Equal(expected, input.Truncate(len));
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(true, "Y")]
        [InlineData(false, "N")]
        public void ToYesNoNullableTests(bool? input, string expected)
        {
            Assert.Equal(expected, input.ToYesNo());
        }

        [Theory]
        [InlineData(true, "Y")]
        [InlineData(false, "N")]
        public void ToYesNoTests(bool input, string expected)
        {
            Assert.Equal(expected, input.ToYesNo());
        }
    }
}