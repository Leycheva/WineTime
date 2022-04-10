namespace WineTime.Tests.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Controllers;
    using WineTime.Tests.Models;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Xunit;
    using Moq;

    public class FavoritesControllerTests
    {
        [Fact]
        public void TryToGetAllFavorites()
        {
            var favoritesController = new FavoritesController(FavoritesServiceMock.Instanse);
            favoritesController.ControllerContext.HttpContext = HttpContextMock.Instance;

            var result = favoritesController.Favorites();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void TryToAddToFavorites()
        {
            var tempData = new TempDataDictionary(HttpContextMock.Instance, Mock.Of<ITempDataProvider>());
            var favoritesController = new FavoritesController(FavoritesServiceMock.Instanse);
            favoritesController.ControllerContext.HttpContext = HttpContextMock.Instance;
            favoritesController.TempData = tempData;

            var result = favoritesController.AddToFavorites(1);

            Assert.NotNull(result);
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void TryToRemoveFromFavorites()
        {
            var tempData = new TempDataDictionary(HttpContextMock.Instance, Mock.Of<ITempDataProvider>());
            var favoritesController = new FavoritesController(FavoritesServiceMock.Instanse);
            favoritesController.ControllerContext.HttpContext = HttpContextMock.Instance;
            favoritesController.TempData = tempData;

            var result = favoritesController.Remove(1);

            Assert.NotNull(result);
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
