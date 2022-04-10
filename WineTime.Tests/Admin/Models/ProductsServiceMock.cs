namespace WineTime.Tests.Admin.Models
{
    using Moq;
    using System.Collections.Generic;
    using WineTime.Core.Constants;
    using Infrastructure.Data;
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
                    .Setup(s => s.Create("", "", "", "", 1, "", 1, Sort.Dry))
                    .Returns(1);

                productsServiceMock
                    .Setup(s => s.CategoryExists(1))
                    .Returns(true);

                productsServiceMock
                    .Setup(s => s.Details(1))
                    .Returns(new ProductsServiceModel());

                productsServiceMock
                   .Setup(s => s.Details(1))
                   .Returns(new ProductsServiceModel());

                return productsServiceMock.Object;
            }
        }
    }
}
