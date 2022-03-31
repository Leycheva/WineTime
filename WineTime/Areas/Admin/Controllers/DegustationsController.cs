namespace WineTime.Areas.Admin.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Areas.Admin.Models;
    using WineTime.Core.Contracts;
    using WineTime.Infrastructure.Data;

    public class DegustationsController : AdminController
    {
        private readonly ApplicationDbContext data;
        private readonly IDegustationsService degustationsService;
        private readonly IMapper mapper;

        public DegustationsController(
            ApplicationDbContext _data,
            IDegustationsService _degustationsService,
            IMapper _mapper)
        {
            data = _data;
            degustationsService = _degustationsService;
            mapper = _mapper;
        }

        public IActionResult Add() => View(new DegustationsFormModel());


        [HttpPost]
        public IActionResult Add(DegustationsFormModel degustation)
        {

            if (!ModelState.IsValid)
            {
                return View(degustation);
            }
            
            degustationsService.Create(
                degustation.Name,
                degustation.Description,
                degustation.Address,
                degustation.DateTime,
                degustation.Seats
               );


            return RedirectToAction("Degustations", "Degustations", new { area = "" });
        }

        public IActionResult Delete(int id)
        {

            degustationsService.Delete(id);

            return RedirectToAction("Degustations", "Degustations", new { area = "" });

        }
    }
}
