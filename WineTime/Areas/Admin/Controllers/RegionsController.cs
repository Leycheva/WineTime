namespace WineTime.Areas.Admin.Models
{
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Areas.Admin.Controllers;
    using WineTime.Infrastructure.Data;

    public class RegionsController : AdminController
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
