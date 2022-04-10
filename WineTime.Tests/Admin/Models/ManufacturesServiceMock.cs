namespace WineTime.Tests.Admin.Models
{
    using Moq;
    using WineTime.Core.Contracts;

    public class ManufacturesServiceMock
    {
        public static IManufacturesService Instanse
        {
            get
            {
                var manufacturesServiceMock = new Mock<IManufacturesService>();

                manufacturesServiceMock
                    .Setup(s => s.Create("", "", 1))
                    .Returns(1);

                manufacturesServiceMock
                    .Setup(s => s.ManufactureExists(1))
                    .Returns(true);

                return manufacturesServiceMock.Object;
            }
        }
    }
}
