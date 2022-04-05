namespace WineTime.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Models.Favorites;
    using WineTime.Extensions;
    using WineTime.Core.Contracts;

    public class FavoritesController : BaseController
    {
        private readonly IFavoritesService favoritesService;

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

            return RedirectToAction("Favorites");
        }

        public IActionResult Remove(int id)
        {
            var userId = User.GetId();

            favoritesService.Remove(userId, id);

            return RedirectToAction("Favorites");
        }
    }
}
