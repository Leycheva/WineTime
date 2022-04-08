namespace WineTime.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Models.Favorites;
    using WineTime.Extensions;
    using WineTime.Core.Contracts;

    using static WebConstants;

    public class FavoritesController : BaseController
    {
        private readonly IFavoritesService favoritesService;

        public FavoritesController(IFavoritesService _favoritesService)
        {
            favoritesService = _favoritesService;
        }

        public IActionResult Favorites()
        {
            string userId = User.GetId();

            var products = favoritesService.GetFavoriteProductsByUser(userId);
           
            return View(new FavoritesFormModel
            {
                Products = products
            });
        }

        public IActionResult AddToFavorites(int id)
        {
            var userId = User.GetId();

            favoritesService.Add(userId, id);

            TempData[GlobalMessageKey] = "You successfully added this product to your Wish List!";

            return RedirectToAction("Favorites");
        }

        public IActionResult Remove(int id)
        {
            var userId = User.GetId();

            favoritesService.Remove(userId, id);

            TempData[GlobalMessageKey] = "You successfully removed this product from your Wish List!";

            return RedirectToAction("Favorites");
        }
    }
}
