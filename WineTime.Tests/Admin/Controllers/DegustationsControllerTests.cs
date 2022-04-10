namespace WineTime.Tests.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Areas.Admin.Controllers;
    using WineTime.Areas.Admin.Models;
    using WineTime.Tests.Models;
    using Xunit;

    public class DegustationsControllerTests
    {
        [Fact]
        public void TryToCreatDegustation()
        {
            var mapper = MapperMock.Instance;
            var degustationsController = new DegustationsController(DegustationsServiceMock.Instanse,mapper);
            degustationsController.ControllerContext.HttpContext = HttpContextMock.Instance;

            var result = degustationsController.Add();
            var result2 = degustationsController.Add(new DegustationsFormModel
            {
                Id = 1,
                Name = "",
                Address = "",
                DateTime = "",
                Description = "",
                Seats = 5
            });

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.NotNull(result2);
            Assert.IsType<RedirectToActionResult>(result2);
        }

        [Fact]
        public void TryToEditDegustation()
        {
            var mapper = MapperMock.Instance;
            var degustationsController = new DegustationsController(DegustationsServiceMock.Instanse, mapper);
            degustationsController.ControllerContext.HttpContext = HttpContextMock.Instance;

            var result = degustationsController.Edit(1);
            var result2 = degustationsController.Edit(new DegustationsFormModel
            {
                Id = 1,
                Name = "",
                Address = "",
                DateTime = "",
                Description = "",
                Seats = 6
            });

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.NotNull(result2);
            Assert.IsType<RedirectToActionResult>(result2);
        }

        [Fact]
        public void TryToDeleteDegustation()
        {
            var mapper = MapperMock.Instance;
            var degustationsController = new DegustationsController(DegustationsServiceMock.Instanse, mapper);
            degustationsController.ControllerContext.HttpContext = HttpContextMock.Instance;

            var result = degustationsController.Delete(1);

            Assert.NotNull(result);
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
