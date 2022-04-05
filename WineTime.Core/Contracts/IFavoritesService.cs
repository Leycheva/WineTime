using WineTime.Core.Models;
using WineTime.Infrastructure.Data;

namespace WineTime.Core.Contracts
{
    public interface IFavoritesService
    {
        Favorite GetFavoritesByUser(string userId);

        IEnumerable<ProductListingServiceModel> GetFavoriteProductsByUser(string userId);

        void Remove(string userId, int id);

        void Add(string userId, int id);
    }
}
