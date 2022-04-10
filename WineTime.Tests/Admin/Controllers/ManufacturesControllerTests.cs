namespace WineTime.Tests.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Areas.Admin.Models;
    using WineTime.Controllers;
    using WineTime.Tests.Admin.Models;
    using WineTime.Tests.Models;
    using Xunit;

    public class ManufacturesControllerTests
    {
        [Fact]
        public void TryToCreatManufacture()
        {
            var manufacturesController = new ManufacturesController(
                ManufacturesServiceMock.Instanse,
                RegionsServiceMock.Instanse);
            manufacturesController.ControllerContext.HttpContext = HttpContextMock.Instance;

            var result = manufacturesController.Add();
            var result2 = manufacturesController.Add(new AddManufactureFormModel
            {
                ImageUrl = "kkkkkkkkk",
                ManufactureName = "kkkkkkkkkkkkkkkk",
                RegionId = 1,
            });
            var result3 = manufacturesController.Add(new AddManufactureFormModel
            {
                ImageUrl = "kkkkkkkkk",
                ManufactureName = "kkkkkkkkkkkkkkkk",
                RegionId = 2,
            });

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.NotNull(result2);
            Assert.IsType<RedirectToActionResult>(result2);
            Assert.NotNull(result3);
            Assert.IsType<ViewResult>(result3);
        }
    }
}
