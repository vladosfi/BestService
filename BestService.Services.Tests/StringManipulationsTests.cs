
namespace BestService.Services.Tests
{
    using BestService.Services.StringHelpers;
    using Xunit;

    public class StringManipulationsTests
    {
        [Theory]
        [InlineData("v1588397888/ggtmr0rydvxdeu2xjcoj.jpg")]
        public void GetNameFromUriWithoutExtensionSouldPassWithoutError(string uri)
        {
            var fileName = StringManipulations.GetNameFromUriWithoutExtension(uri);

            Assert.Equal("ggtmr0rydvxdeu2xjcoj", fileName);
        }

        [Fact]
        public void GetNameFromUriWithoutExtensionSouldReturnNull()
        {
            var fileName = StringManipulations.GetNameFromUriWithoutExtension(string.Empty);

            Assert.Null(fileName);
        }
    }
}
