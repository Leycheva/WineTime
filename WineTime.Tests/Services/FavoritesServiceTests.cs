namespace WineTime.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using WineTime.Core.Contracts;
    using WineTime.Core.Services;
    using WineTime.Infrastructure.Data;
    using WineTime.Tests.Models;
    using Xunit;

    public class FavoritesServiceTests
    {
        [Fact]
        public void TryToAddPrdductToFavorites()
        {
            var data = DatabaseMock.Instance;
            var favoritesService = GetService(data);

            favoritesService.Add("TestId", 1);
            favoritesService.Add("TestId2", 1);
            favoritesService.Add("TestId", 15);

            var result = data.Favorites.FirstOrDefault(x => x.UserId == "TestId");

            Assert.NotNull(result);
            Assert.Equal(1,result.Products.Count);
            Assert.Contains(result.Products,x => x.Id == 1);
        }

        [Fact]
        public void TryToRemoveFromFavorites()
        {
            var data = DatabaseMock.Instance;
            var favoritesService = GetService(data);

            favoritesService.Remove("TestId", 1);
            favoritesService.Remove("TestId2", 1);
            favoritesService.Remove("TestId", 15);

            var result = data.Favorites.FirstOrDefault(x => x.UserId == "TestId");

            Assert.Null(result);
        }

        [Fact]
        public void TryToGetFavoritesProductsByUser()
        {
            var data = DatabaseMock.Instance;
            var favoritesService = GetService(data);

            favoritesService.Add("TestId", 1);

            var result = favoritesService.GetFavoriteProductsByUser("TestId");
            var result2 = favoritesService.GetFavoriteProductsByUser("TestId2");

            Assert.NotNull(result);
            Assert.Null(result2);
            Assert.Equal(1, result.Count());
            Assert.Contains(result, x => x.Id == 1);

        }

            private static IFavoritesService GetService(ApplicationDbContext outdata = null)
        {
            var data = outdata;
            if (data == null)
            {
                data = DatabaseMock.Instance;
            }
            var favoritesService = new FavoritesService(data);

            var category = new Category { Id = 1, Name = "Red", Products = new HashSet<Product>() };
            data.Categories.Add(category);
            var region = new Region { Id = 1, Country = "BG" };
            data.Regions.Add(region);
            var manufacture = new Manufacture { Id = 1, ManufactureName = "Katarzina", Region = region, ImageUrl = "",Products = new HashSet<Product>() };
            data.Manufactures.Add(manufacture);
            data.Products.Add(new Product
            {
                Id = 1,
                Name = "Mezzek",
                ImageUrl = "",
                Region = region,
                Manufacture = manufacture,
                Category = category,
                Description = "Some text",
                Price = 20,
                YearOfManufacture = "2020",
                Sort = Sort.Dry,
            });

            var user = new ApplicationUser { Id = "TestId" };
            data.Users.Add(user);

            data.SaveChanges();

            return favoritesService;
        }
    }
}
