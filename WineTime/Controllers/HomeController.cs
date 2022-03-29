namespace WineTime.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using WineTime.Areas.Admin.Models;
    using WineTime.Infrastructure.Data;
    using WineTime.Models;

    public class HomeController : BaseController
    {

        private readonly ApplicationDbContext data;

        private readonly ILogger<HomeController> logger;

        public HomeController(ILogger<HomeController> _logger, ApplicationDbContext _data)
        {
            logger = _logger;
            data = _data;
        }

        public IActionResult Index()
        {
            var products = data
                .Products
                .OrderByDescending(p => p.Id)
                .Select(p => new ProductListingViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.Name,
                    Price = p.Price,
                    YearOfManufacture = p.YearOfManufacture

                })
                .Take(3)
                .ToList();

            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}