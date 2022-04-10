namespace WineTime.Tests.Models
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
   
    using Moq;
    using System.Security.Claims;
    using WineTime.Infrastructure.Data;

    public static class HttpContextMock
    {
        public static DefaultHttpContext Instance
        {
            get
            {
                var claim = new Claim("test", "TestId");
                var mockIdentity = Mock.Of<ClaimsIdentity>(ci => ci.FindFirst(It.IsAny<string>()) == claim);
                var userStore = new Mock<IUserStore<ApplicationUser>>();

                var userManager = new UserManager<ApplicationUser>(
                                 userStore.Object, null, null, null, null, null, null, null, null);

                var cp = new Mock<ClaimsPrincipal>();
                cp.Setup(m => m.HasClaim(It.IsAny<string>(), It.IsAny<string>()))
                  .Returns(true);
                cp.Setup(m => m.Identity).Returns(mockIdentity);

                var context = new DefaultHttpContext
                {
                    User = cp.Object
                };

                
                return context;
            }
        }
    }
}
