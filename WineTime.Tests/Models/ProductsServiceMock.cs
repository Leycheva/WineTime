namespace WineTime.Tests.Models
{

    using Moq;
    using System.Collections.Generic;
    using WineTime.Core.Constants;
    using WineTime.Core.Contracts;
    using WineTime.Core.Models;
    using WineTime.Core.Models.Degustations;
    

    public class ProductsServiceMock
    {
        public static IProductsService Instanse
        {
            get
            {
                var productsServiceMock = new Mock<IProductsService>();

                productsServiceMock
                    .Setup(s => s.All("Red", "", "", ProductSorting.Manufacture, 6, 1))
                    .Returns(new ProductQueryServiceModel
                    {
                        CurrentPage = 1,
                        ProductPerPage = 3,
                        TotalProducts = 3,
                        Products = new List<ProductListingServiceModel>()
                    });

                productsServiceMock
                    .Setup(s => s.Details(1))
                    .Returns(new ProductsServiceModel
                    {
                        Id = 1
                    });

                return productsServiceMock.Object;
            }
        }
    }
}
