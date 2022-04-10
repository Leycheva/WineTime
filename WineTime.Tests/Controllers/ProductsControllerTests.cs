namespace WineTime.Tests.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Areas.Admin.Models;
    using WineTime.Core.Models;
    using WineTime.Tests.Models;
    using Xunit;

    public class ProductsControllerTests
    {
        [Fact]
        public void TryToGetAllProducts()
        {
            var mapper = MapperMock.Instance;
            var productsController = new WineTime.Controllers.ProductsController(ProductsServiceMock.Instanse,mapper);
            productsController.ControllerContext.HttpContext = HttpContextMock.Instance;

            var result = productsController.All(new AllProductQueryModel
            {
                CurrentPage = 1,
                Name = "",
                PageCount = 1,
                Category = "Red",
                SearchTerm = "",
                Sorting = ProductSorting.Manufacture
            }); 

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void TryToGetProductDetails()
        {
            var mapper = MapperMock.Instance;
            var productsController = new WineTime.Controllers.ProductsController(ProductsServiceMock.Instanse, mapper);
            productsController.ControllerContext.HttpContext = HttpContextMock.Instance;

            var result = productsController.Details(1);

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
