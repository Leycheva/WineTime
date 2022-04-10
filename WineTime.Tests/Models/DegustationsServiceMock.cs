namespace WineTime.Tests.Models
{
    using Moq;
    using System.Collections.Generic;
    using WineTime.Core.Contracts;
    using WineTime.Core.Models.Degustations;

    public static class DegustationsServiceMock
    {
        public static IDegustationsService Instanse
        {
            get
            {
                var degustationsServiceMock = new Mock<IDegustationsService>();

                degustationsServiceMock
                    .Setup(s => s.Book("TestId", 1))
                    .Returns(true);


                degustationsServiceMock
                    .Setup(s => s.All(3, 1))
                    .Returns(new DegustationsQueryServiceModel
                    {
                         CurrentPage = 1,
                         DegustationPerPage = 2,
                         TotalDegustations = 1,
                         Degustations = new List<DegustationsServiceViewModel>
                         {
                             new DegustationsServiceViewModel
                             {
                                 Id = 1,
                                 Name = "Text",
                                 Address = "Address",
                                 Description = "",
                                 DateTime = "",
                                 Seats = 5,
                                 BookSeats = 2,
                             }
                         }
                    });

                return degustationsServiceMock.Object;
            }
        }
    }
}
