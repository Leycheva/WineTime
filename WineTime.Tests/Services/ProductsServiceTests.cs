namespace WineTime.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using WineTime.Core.Constants;
    using WineTime.Core.Models;
    using WineTime.Core.Services;
    using WineTime.Infrastructure.Data;
    using WineTime.Tests.Models;
    using Xunit;

    public class ProductsServiceTests
    {
        [Fact]
        public void IsCategoryExists()
        {
            var data = DatabaseMock.Instance;
            var productsService = GetService(data);

            var result = productsService.CategoryExists(1);
            var result2 = productsService.CategoryExists(51);

            Assert.True(result);
            Assert.False(result2);
        }

        [Fact]
        public void TryToGetProductCategory()
        {
            var data = DatabaseMock.Instance;
            var productsService = GetService(data);

            var result = productsService.GetProductCategories();

            Assert.NotNull(result);
            Assert.Contains(result, x => x.Id == 1);
            Assert.Equal(1, result.Count());
        }

        [Fact]
        public void TryToGetProductManufacture()
        {
            var data = DatabaseMock.Instance;
            var productsService = GetService(data);

            var result = productsService.GetProductManufactures();

            Assert.NotNull(result);
            Assert.Contains(result, x => x.Id == 1);
            Assert.Equal(1, result.Count());
        }

        [Fact]
        public void TryToGetProductRegion()
        {
            var data = DatabaseMock.Instance;
            var productsService = GetService(data);

            var result = productsService.GetProductRegion();

            Assert.NotNull(result);
            Assert.Contains(result, x => x.Id == 1);
            Assert.Equal(1, result.Count());
        }

        [Fact]
        public void TryToGetDetailsAboutProduct()
        {
            var productsService = GetService();

            var result = productsService.Details(1);

            Assert.NotNull(result);
            Assert.True(result.Name == "Mezzek");
            Assert.True(result.YearOfManufacture == "2020");
        }

        [Fact]
        public void TryToCreateProduct()
        {
            var data = DatabaseMock.Instance;
            var productsService = GetService(data);

            var id = productsService.Create("Villa Yambol", "20", "", "Some text", 1, "2005", 1, Sort.Dry);

            var result = data.Products.FirstOrDefault(x => x.Id == id);

            Assert.NotNull(result);
            Assert.Equal("Villa Yambol", result.Name);
            Assert.Equal("Some text", result.Description);
            Assert.Equal("2005", result.YearOfManufacture);
        }

        [Fact]
        public void TryToUpdateProduct()
        {
            var data = DatabaseMock.Instance;
            var productsService = GetService(data);

            productsService.Update(1,"Villa Sitnica", "22", "", "Some new text", 1, "2008", 1, Sort.Sweet);

            var result = data.Products.FirstOrDefault(x => x.Id == 1);

            Assert.NotNull(result);
            Assert.Equal("Villa Sitnica", result.Name);
            Assert.Equal("Some new text", result.Description);
            Assert.Equal("2008", result.YearOfManufacture);
        }

        [Fact]
        public void TryToDeleteProduct()
        {
            var data = DatabaseMock.Instance;
            var productsService = GetService();

            productsService.Delete(1);
            productsService.Delete(13);

            Assert.DoesNotContain(data.Products, x => x.Id == 1);
        }

        [Fact]
        public void IsReturnAListOfAllProducts()
        {
            var productsService = GetService();

            var result = productsService.All("Rose", "Some text", "Some name", ProductSorting.Price, 1, 1);

            Assert.NotNull(result);
            Assert.Equal(1, result.CurrentPage);
            Assert.Equal(1, result.ProductPerPage);
        }

        private static IProductsService GetService(ApplicationDbContext outdata = null)
        {
            var mapper = MapperMock.Instance;
            var data = outdata;
            if (data == null)
            {
                data = DatabaseMock.Instance;
            }
            var productsService = new ProductsService(data,mapper);

            var category = new Category { Id = 1, Name = "Red", Products = new HashSet<Product>() };
            data.Categories.Add(category);
            var region = new Region { Id = 1, Country = "BG" };
            data.Regions.Add(region);
            var manufacture = new Manufacture { Id = 1, ManufactureName = "Katarzina", Region = region, ImageUrl = "", Products = new HashSet<Product>() };
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

            return productsService;
        }
    }
}
