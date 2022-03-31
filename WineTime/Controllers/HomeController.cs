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

        public HomeController(ApplicationDbContext _data)
        {
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

        public IActionResult Error()
        {
            return View();
        }
    }
}