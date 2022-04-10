namespace WineTime.Tests.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Models.Degustations;
    using WineTime.Controllers;
    using WineTime.Tests.Models;
    using Xunit;

    public class DegustationsControllerTests
    {
        [Fact]
        public void BookShouldReturnView()
        {
            var degustationsController = new DegustationsController(DegustationsServiceMock.Instanse);
            degustationsController.ControllerContext.HttpContext = HttpContextMock.Instance;

            var result = degustationsController.Book(1);
            var result2 = degustationsController.Book(2);

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.NotNull(result2);
            Assert.IsType<ViewResult>(result2);
        }

        [Fact]
        public void TryToGetAllDegustations()
        {
            var degustationsController = new DegustationsController(DegustationsServiceMock.Instanse);
            degustationsController.ControllerContext.HttpContext = HttpContextMock.Instance;

            var result = degustationsController.Degustations(new AllDegustationQueryModel
            {
                CurrentPage = 1,
                PageCount = 1,
            });

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
