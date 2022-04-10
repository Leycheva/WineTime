namespace WineTime.Areas.Admin.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Areas.Admin.Models;
    using WineTime.Core.Contracts;

    public class DegustationsController : AdminController
    {
        private readonly IDegustationsService degustationsService;
        private readonly IMapper mapper;

        public DegustationsController(
            IDegustationsService _degustationsService,
            IMapper _mapper)
        {
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

        public IActionResult Edit(int id)
        {
            var degustation = degustationsService.Details(id);
            var degustationForm = mapper.Map<DegustationsFormModel>(degustation);

            return View(degustationForm);
        }

        [HttpPost]
        public IActionResult Edit(DegustationsFormModel degustation)
        {
            if (!ModelState.IsValid)
            {
                return View(degustation);
            }


            degustationsService.Update(
                degustation.Id,
                degustation.Name,
                degustation.Description,
                degustation.Address,
                degustation.DateTime,
                degustation.Seats);

            return RedirectToAction("Degustations", "Degustations", new { area = "" });
        }

        public IActionResult Delete(int id)
        {

            degustationsService.Delete(id);

            return RedirectToAction("Degustations", "Degustations", new { area = "" });

        }
    }
}
