namespace WineTime.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Core.Contracts;
    using WineTime.Infrastructure.Data;
    using WineTime.Models.Degustations;

    public class DegustationsController : BaseController
    {
        private readonly ApplicationDbContext data;
        private readonly IDegustationsService degustationsService;

        public DegustationsController(
            ApplicationDbContext _data,
            IDegustationsService _degustationsService)
        {
            data = _data;
            degustationsService = _degustationsService;
        }

        public IActionResult Degustations()
        {
           var degustations = degustationsService.All();  
           var model =new DegustationsAllViewModel { Degustations = degustations };

            return View(model);
        }
    }
}
