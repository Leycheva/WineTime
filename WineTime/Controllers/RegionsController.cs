namespace WineTime.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Infrastructure.Data;
    using WineTime.Models.Regions;

    public class RegionsController : Controller
    {

        private readonly ApplicationDbContext data;

        public RegionsController(ApplicationDbContext _data) => data = _data;

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddRegionFormModel region)
        {
            if (!ModelState.IsValid)
            {
                return View(region);
            }

            var regionData = data.Regions.FirstOrDefault();

            var newRegion = new Region
            {
                Country = region.Country,
            };

            data.Regions.Add(newRegion);
            data.SaveChanges();

            return RedirectToAction("Index","Home");
        }
    }
}
