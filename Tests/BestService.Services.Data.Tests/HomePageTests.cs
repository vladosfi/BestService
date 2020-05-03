namespace BestService.Services.Data.Tests
{
    using System.Threading.Tasks;

    using BestService.Web;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Xunit;

    public class HomePageTests
    {
        [Fact]
        public async Task HomePageShuldHaveH1Title()
        {
            var serverFactory = new WebApplicationFactory<Startup>();
            var client = serverFactory.CreateClient();

            var response = await client.GetAsync("/");
            var responseAsString = await response.Content.ReadAsStringAsync();
            Assert.Contains("<h1", responseAsString);
        }
    }
}
