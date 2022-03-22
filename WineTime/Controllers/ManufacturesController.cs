namespace WineTime.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Infrastructure.Data;
    using WineTime.Models.Manufactures;

    public class ManufacturesController : Controller
    {

        private readonly ApplicationDbContext data;
        public ManufacturesController(ApplicationDbContext _data) => data = _data;

        public IActionResult Add() => View(new AddManufactureFormModel
        {
            Regions = GetManufactureRegions()
        });

        [HttpPost]
        public IActionResult Add(AddManufactureFormModel manufacture)
        {

            if (!data.Regions.Any(m => m.Id == manufacture.RegionId))
            {
                ModelState.AddModelError(nameof(manufacture.RegionId), "Region does not exist.");
            }

            if (!ModelState.IsValid)
            {
                manufacture.Regions = GetManufactureRegions();
                return View(manufacture);
            }

            var manufactureData = new Manufacture
            {
                ManufactureName = manufacture.ManufactureName,
                ImageUrl = manufacture.ImageUrl,
                RegionId = manufacture.RegionId
            };

            data.Manufactures.Add(manufactureData);
            data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

            private IEnumerable<ManufactureRegionViewModel> GetManufactureRegions()
          => this.data
           .Regions
           .Select(c => new ManufactureRegionViewModel
           {
               Id = c.Id,
               Country = c.Country
           })
           .ToList();
    }
}
