namespace WineTime.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using WineTime.Core.Contracts;
    using WineTime.Core.Models;
    using WineTime.Infrastructure.Data;

    public class FavoritesService : IFavoritesService
    {
        private readonly ApplicationDbContext data;

        public FavoritesService(ApplicationDbContext _data) => data = _data;

        public void Add(string userId, int id)
        {
            if (!data.Users.Any(x => x.Id == userId))
            {
                return;
            }
            var product = data.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return;
            }
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
        }

        public IEnumerable<ProductListingServiceModel> GetFavoriteProductsByUser(string userId)
        {
            var favorite = GetFavoritesByUser(userId);

            if (favorite == null)
            {
                return null;
            }

            var productCategorie = data
                .Products
                .Select(p => p.Category.Name)
                .FirstOrDefault();

            var products = favorite
                .Products
                .Select(p => new ProductListingServiceModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    YearOfManufacture = p.YearOfManufacture,
                    Category = productCategorie
                }).ToList();

            return products;
        }

        public Favorite? GetFavoritesByUser(string userId)
            => data
             .Favorites
             .Include(x => x.Products)
             .FirstOrDefault(f => f.UserId == userId);


        public void Remove(string userId, int id)
        {
            if(!data.Users.Any(x => x.Id == userId))
            {
                return;
            }

            var favorite = GetFavoritesByUser(userId);
            if (favorite == null)
            {
                return;
            }

            var product = data.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return;
            }

            favorite.Products.Remove(product);
            data.Update(favorite);
            data.SaveChanges();
        }
    }
}
