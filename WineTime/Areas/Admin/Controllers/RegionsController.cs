namespace WineTime.Areas.Admin.Models
{
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Areas.Admin.Controllers;
    using WineTime.Core.Contracts;

    public class RegionsController : AdminController
    {
        private readonly IRegionsService regionsService;

        public RegionsController(IRegionsService _regionsService) => regionsService = _regionsService;

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddRegionFormModel region)
        {
            if (!ModelState.IsValid)
            {
                return View(region);
            }

            regionsService.Create(region.Country);

            return RedirectToAction("All", "Products", new { area = "" });
        }
    }
}
