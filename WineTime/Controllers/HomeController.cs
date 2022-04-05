namespace WineTime.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using WineTime.Areas.Admin.Models;
    using WineTime.Infrastructure.Data;
    using WineTime.Models;

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