namespace WineTime.Tests.Admin.Models
{
    using Moq;
    using WineTime.Core.Contracts;

    public static class DegustationsServiceMock
    {
        public static IDegustationsService Instanse
        {
            get
            {
                var degustationsServiceMock = new Mock<IDegustationsService>();

                degustationsServiceMock
                    .Setup(s => s.Create("", "", "", "", 5))
                    .Returns(1);


                return degustationsServiceMock.Object;
            }
        }
    }
}
