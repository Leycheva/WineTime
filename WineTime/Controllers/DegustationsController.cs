namespace WineTime.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Core.Contracts;
    using WineTime.Extensions;
    using WineTime.Models.Degustations;

    public class DegustationsController : BaseController
    {
        private readonly IDegustationsService degustationsService;

        public DegustationsController(IDegustationsService _degustationsService) 
            => degustationsService = _degustationsService;
        
        public IActionResult Degustations([FromQuery] AllDegustationQueryModel query)
        {
            var queryResult = degustationsService.All(
                AllDegustationQueryModel.DegustationPerPage,
                query.CurrentPage);

            query.Degustations = queryResult.Degustations;
            query.TotalDegustation = queryResult.TotalDegustations;

            return View(query);
        }

        public IActionResult Book(int id)
        {
            var userId = User.GetId();

           var success = degustationsService.Book(userId, id);

            if (success)
            {
                return View("DegError");
            }

            return View("Book");
        }
    }
}
