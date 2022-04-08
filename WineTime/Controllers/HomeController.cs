namespace WineTime.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    public class HomeController : BaseController
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}