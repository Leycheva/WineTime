namespace WineTime.Tests.Services
{
    using System.Linq;
    using WineTime.Core.Services;
    using WineTime.Infrastructure.Data;
    using WineTime.Tests.Models;
    using Xunit;

    public class ManufacturesServiceTests
    {
        [Fact]
        public void TryToAddManufacture()
        {
            var data = DatabaseMock.Instance;
            var region = new Region { Id = 1, Country = "BG" };
            data.Regions.Add(region);
            data.SaveChanges();

            var manufactureService = new ManufacturesService(data);

            var id = manufactureService.Create("TestName", "www.abc-123.com", 1);

            var result = data.Manufactures.FirstOrDefault(x => x.Id == id);

            Assert.NotNull(result);
            Assert.Equal("TestName", result.ManufactureName);
            Assert.Equal(1, result.RegionId);
            Assert.Equal("www.abc-123.com", result.ImageUrl);
        }

        [Fact]
        public void TryToGetRegions()
        {
            var data = DatabaseMock.Instance;
            var region = new Region { Id = 1, Country = "BG" };
            var region2 = new Region { Id = 2, Country = "FR" };
            var region3 = new Region { Id = 3, Country = "SP" };
            data.Regions.Add(region);
            data.Regions.Add(region2);
            data.Regions.Add(region3);
            data.SaveChanges();

            var manufactureService = new ManufacturesService(data);

            var result = manufactureService.GetManufactureRegions().ToList();

            Assert.Equal(3, result.Count);
            Assert.Contains(result, x => x.Id == 1);
            Assert.Contains(result, x => x.Id == 2);
            Assert.Contains(result, x => x.Id == 3);
            Assert.Contains(result, x => x.Country == "BG");
            Assert.Contains(result, x => x.Country == "FR");
            Assert.Contains(result, x => x.Country == "SP");
        }


        [Fact]
        public void IfRegionExist()
        {
            var data = DatabaseMock.Instance;
            var region = new Region { Id = 1, Country = "BG" };
            data.Regions.Add(region);
            data.SaveChanges();

            var manufactureService = new ManufacturesService(data);

            var result = manufactureService.RegionExists(1);
            var result2 = manufactureService.RegionExists(2);

            Assert.True(result);
            Assert.False(result2);
        }
    }
}
