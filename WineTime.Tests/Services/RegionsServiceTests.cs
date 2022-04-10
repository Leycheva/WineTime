namespace WineTime.Tests.Services
{
    using System.Linq;
    using WineTime.Core.Services;
    using WineTime.Infrastructure.Data;
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

        [Fact]
        public void IsRegionExist()
        {
            var data = DatabaseMock.Instance;
            var region = new Region { Id = 1, Country = "BG" };
            data.Regions.Add(region);
            data.SaveChanges();

            var regionService = new RegionsService(data);

            var result = regionService.RegionExists(1);
            var result2 = regionService.RegionExists(2);

            Assert.True(result);
            Assert.False(result2);
        }
    }
}
