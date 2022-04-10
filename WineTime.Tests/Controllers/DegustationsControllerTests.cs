namespace WineTime.Tests.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Controllers;
    using WineTime.Tests.Models;
    using Xunit;

    public class DegustationsControllerTests
    {
        [Fact]
        public void BookShouldReturnView()
        {
            var mapper = MapperMock.Instance;

            var degustationsController = new DegustationsController(DegustationsServiceMock.Instanse);

            var result = degustationsController.Book(1);

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);

        }
    }
}
