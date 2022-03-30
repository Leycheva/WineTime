namespace WineTime.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Claims;
    using WineTime.Areas.Admin.Models;
    using WineTime.Infrastructure.Data;
    using WineTime.Models.Favorites;

    public class FavoritesController : BaseController
    {
        private readonly ApplicationDbContext data;

        public FavoritesController(ApplicationDbContext _data)
        {
            data = _data;
        }

        public IActionResult Favorites()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var favorite = data
                .Favorites
                .Include(x => x.Products)
                .FirstOrDefault(f => f.UserId == userId);

            if (favorite == null)
            {
                return View(new FavoritesFormModel());
            }

            var quary = favorite.Products.AsQueryable();

            var productCategorie = data
                .Products
                .Select(p => p.Category.Name)
                .FirstOrDefault();

            var products = favorite
                .Products
                .Select(p => new ProductListingViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    YearOfManufacture = p.YearOfManufacture,
                    Category = productCategorie
                }).ToList();

            return View(new FavoritesFormModel
            {
                Products = products
            });
        }

        public IActionResult AddToFavorites(int id)
        {
            var product = data.Products.FirstOrDefault(p => p.Id == id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (data.Favorites.Any(x => x.UserId == userId))
            {
                var favorite = data.Favorites.FirstOrDefault(x => x.UserId == userId);

                favorite.Products.Add(product);

                data.Update(favorite);
                data.SaveChanges();
            }
            else
            {
                var favorite = new Favorite
                {
                    UserId = userId,
                    Products =
                    {
                        product
                    }
                };

                data.Favorites.Add(favorite);
                data.SaveChanges();
            }

            return RedirectToAction("Favorites");
        }

        public IActionResult Remove(int id)
        {

            Console.WriteLine(id);
            var product = data.Products.FirstOrDefault(p => p.Id == id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var favorite = data.Favorites.Include(x => x.Products).FirstOrDefault(x => x.UserId == userId);
            favorite.Products.Remove(product);

            data.Update(favorite);
            data.SaveChanges();

            return RedirectToAction("Favorites");
        }
    }
}
