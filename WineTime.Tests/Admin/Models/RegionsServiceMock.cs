namespace WineTime.Tests.Admin.Models
{
    using Moq;
    using WineTime.Core.Contracts;

    public class RegionsServiceMock
    {
        public static IRegionsService Instanse
        {
            get
            {
                var regionsServiceMock = new Mock<IRegionsService>();

                regionsServiceMock
                    .Setup(s => s.Create("BG"))
                    .Returns(1);

                regionsServiceMock
                    .Setup(s => s.RegionExists(1))
                    .Returns(true);


                return regionsServiceMock.Object;
            }
        }
    }
}
