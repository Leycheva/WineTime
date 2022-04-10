namespace WineTime.Tests.Models
{
    using Moq;
    using System.Collections.Generic;
    using WineTime.Core.Contracts;
    using WineTime.Core.Models;

    public class FavoritesServiceMock
    {
        public static IFavoritesService Instanse
        {
            get
            {
                var favoritesServiceMock = new Mock<IFavoritesService>();

                favoritesServiceMock
                    .Setup(f => f.GetFavoriteProductsByUser("TestId"))
                    .Returns(new List<ProductListingServiceModel>());

                return favoritesServiceMock.Object;
            }
        }
    }
}
