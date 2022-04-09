namespace WineTime.Tests.Services
{
    using System;
    using System.Linq;
    using WineTime.Core.Contracts;
    using WineTime.Core.Services;
    using WineTime.Infrastructure.Data;
    using WineTime.Tests.Models;
    using Xunit;

    public class DegustationsServiceTests
    {
        [Fact]
        public void IsReturnAListOfAllDegustatiions()
        {
            var degustationsServise = GetService();

            var result = degustationsServise.All().ToList();
            var isExist = result.Any(x => x.Id == 1);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.True(isExist);
        }

        [Fact]
        public void TryToAddDegustation()
        {
            var data = DatabaseMock.Instance;
            var degustationsServise = GetService(data);

            var id = degustationsServise.Create("Test", "Some description", "Sofia", DateTime.Now.ToString(), 5);

            var result = data.Degustations.FirstOrDefault(x => x.Id == id);

            Assert.NotNull(result);
            Assert.Equal("Sofia", result.Address);
            Assert.Equal(5, result.Seats);
        }

        [Fact]
        public void TryToDeleteDegustation()
        {
            var data = DatabaseMock.Instance;
            var degustationsServise = GetService();

            degustationsServise.Delete(1);

            Assert.DoesNotContain(data.Degustations, x => x.Id == 1);
        }

        [Fact]
        public void TryToGetDetailsAboutDegustation()
        {
            var degustationsServise = GetService();

            var result = degustationsServise.Details(1);

            Assert.NotNull(result);
            Assert.True(result.Name =="Wine-1");
        }

        [Fact]
        public void TryToBookADegustation()
        {
            var data = DatabaseMock.Instance;
            var degustationsServise = GetService(data);

            var result = degustationsServise.Book("TestId", 1);

            Assert.True(result);
            Assert.Contains(data.UserDegustatuions, x => x.UserId == "TestId" && x.DegustationId == 1);
        }

        [Fact]
        public void TryToBookInvalidDegustation()
        {
            var data = DatabaseMock.Instance;
            var degustationsServise = GetService(data);

            degustationsServise.Book("TestId", 1);

            var result1 = degustationsServise.Book("TestId", 3);
            var result2 = degustationsServise.Book("hwhw", 1);
            var result3 = degustationsServise.Book("TestId", 1);

            Assert.False(result1);
            Assert.False(result2);
            Assert.False(result3);
        }

        private static IDegustationsService GetService(ApplicationDbContext outdata = null)
        {
            var data = outdata;
            if (data == null)
            {
                data = DatabaseMock.Instance;
            }
            var mapper = MapperMock.Instance;

            var degustationsServise = new DegustationsService(data, mapper);

            data.Degustations.Add(new Degustation
            {
                Id = 1,
                Address = "Sofia",
                Name = "Wine-1",
                Description = "Some description"
            });
            data.Degustations.Add(new Degustation
            {
                Id = 2,
                Address = "Sofia",
                Name = "Wine-2",
                Description = "Some description"
            });

            var user = new ApplicationUser { Id = "TestId" };
            data.Users.Add(user);
            data.SaveChanges();
            
            return degustationsServise;
        }
    }
}
