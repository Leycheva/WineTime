namespace WineTime.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Areas.Admin.Controllers;
    using WineTime.Areas.Admin.Models;
    using WineTime.Core.Contracts;

    public class ManufacturesController : AdminController
    {
        private readonly IManufacturesService manufactureService;

        public ManufacturesController(IManufacturesService _manufactureService) 
            => manufactureService = _manufactureService;

        public IActionResult Add() => View(new AddManufactureFormModel
        {
            Regions = manufactureService.GetManufactureRegions()
        });

        [HttpPost]
        public IActionResult Add(AddManufactureFormModel manufacture)
        {

            if (!manufactureService.RegionExists(manufacture.RegionId))
            {
                ModelState.AddModelError(nameof(manufacture.RegionId), "Region does not exist.");
            }

            if (!ModelState.IsValid)
            {
                manufacture.Regions = manufactureService.GetManufactureRegions();
                return View(manufacture);
            }

            manufactureService.Create(
                manufacture.ManufactureName,
                manufacture.ImageUrl,
                manufacture.RegionId);

            return RedirectToAction("All", "Products", new { area = "" });
        }
    }
}
