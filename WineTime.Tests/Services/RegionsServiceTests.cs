namespace WineTime.Tests.Services
{
    using System.Linq;
    using WineTime.Core.Services;
    using WineTime.Tests.Models;
    using Xunit;

    public class RegionsServiceTests
    {
        [Fact]
        public void TryToAddRegion()
        {
            var data = DatabaseMock.Instance;

            var regiionService = new RegionsService(data);

            var id = regiionService.Create("BG");

            var result = data.Regions.FirstOrDefault(x => x.Id == id);

            Assert.NotNull(result);
            Assert.Equal("BG", result.Country);
        }
    }
}
