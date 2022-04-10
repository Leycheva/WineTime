namespace WineTime.Tests.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Areas.Admin.Models;
    using WineTime.Controllers;
    using WineTime.Tests.Admin.Models;
    using WineTime.Tests.Models;
    using Xunit;

    public class RegionsControllerTests
    {
        [Fact]
        public void TryToCreatRegion()
        {
            var regionsController = new RegionsController( RegionsServiceMock.Instanse);
            regionsController.ControllerContext.HttpContext = HttpContextMock.Instance;

            var result = regionsController.Add();
            var result2 = regionsController.Add(new AddRegionFormModel
            {
                Country = "BG"
            });
            regionsController.ModelState.AddModelError("1", "Error");
            var result3 = regionsController.Add(new AddRegionFormModel
            {
                Country = "FR"
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
