namespace WineTime.Tests.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Areas.Admin.Models;
    using WineTime.Tests.Admin.Models;
    using WineTime.Tests.Models;
    using Xunit;

    public class ProductsControllerTests
    {
        [Fact]
        public void TryToAddProduct()
        {
            var mapper = MapperMock.Instance;
            var productsController = new ProductsController(Admin.Models.ProductsServiceMock.Instanse, 
                ManufacturesServiceMock.Instanse, 
                mapper);
            productsController.ControllerContext.HttpContext = HttpContextMock.Instance;

            var result = productsController.Add();
            var result2 = productsController.Add( new ProductFormModel
            {
                Id = 1,
                Description = "",
                ManufactureId = 1,
                CategoryId = 1,
                ImageUrl = "",
                Name = ""
            });
            var result3 = productsController.Add(new ProductFormModel
            {
                Id = 2,
                Description = "",
                ManufactureId = 1,
                CategoryId = 5,
                ImageUrl = "",
                Name = ""
            });
            var result4 = productsController.Add(new ProductFormModel
            {
                Id = 2,
                Description = "",
                ManufactureId = 10,
                CategoryId = 1,
                ImageUrl = "",
                Name = ""
            });

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.NotNull(result2);
            Assert.IsType<RedirectToActionResult>(result2);
            Assert.NotNull(result3);
            Assert.IsType<ViewResult>(result3);
            Assert.NotNull(result4);
            Assert.IsType<ViewResult>(result4);
        }

        [Fact]
        public void TryToEditProduct()
        {
            var mapper = MapperMock.Instance;
            var productsController = new ProductsController(Admin.Models.ProductsServiceMock.Instanse,
                ManufacturesServiceMock.Instanse,
                mapper);
            productsController.ControllerContext.HttpContext = HttpContextMock.Instance;

            var result = productsController.Edit(1);
            var result2 = productsController.Edit(new ProductFormModel
            {
                Id = 1,
                Description = "",
                ManufactureId = 1,
                CategoryId = 1,
                ImageUrl = "",
                Name = "Aaaaa"
            });
            var result3 = productsController.Edit(new ProductFormModel
            {
                Id = 1,
                Description = "",
                ManufactureId = 1,
                CategoryId = 5,
                ImageUrl = "",
                Name = ""
            });
            var result4 = productsController.Edit(new ProductFormModel
            {
                Id = 1,
                Description = "",
                ManufactureId = 10,
                CategoryId = 1,
                ImageUrl = "",
                Name = ""
            });

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.NotNull(result2);
            Assert.IsType<RedirectToActionResult>(result2);
            Assert.NotNull(result3);
            Assert.IsType<ViewResult>(result3);
            Assert.NotNull(result4);
            Assert.IsType<ViewResult>(result4);
        }

        [Fact]
        public void TryToDeleteProduct()
        {
            var mapper = MapperMock.Instance;
            var productsController = new ProductsController(Admin.Models.ProductsServiceMock.Instanse,
                ManufacturesServiceMock.Instanse,
                mapper);
            productsController.ControllerContext.HttpContext = HttpContextMock.Instance;

            var result = productsController.Delete(1);

            Assert.NotNull(result);
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
