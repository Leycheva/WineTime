namespace WineTime.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Infrastructure.Data;

    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UsersController(
            UserManager<ApplicationUser> _userManager
            , SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }
    }

}
