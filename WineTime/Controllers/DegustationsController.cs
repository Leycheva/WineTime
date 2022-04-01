namespace WineTime.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Areas.Admin.Models;
    using WineTime.Core.Contracts;
    using WineTime.Core.Models;
    using WineTime.Infrastructure.Data;
    using WineTime.Models.Degustations;

    public class DegustationsController : BaseController
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

        public IActionResult Degustations([FromQuery] AllDegustationQueryModel query)
        {
            var degQuery = data.Degustations.AsQueryable();
            //var degustations = degustationsService.All();
            //var model =new DegustationsAllViewModel { Degustations = degustations };
            var totalDeg = degQuery.Count();

            var degustations = degQuery
                .Skip((query.CurrentPage - 1) * AllDegustationQueryModel.DegustationPerPage)
                .Take(AllDegustationQueryModel.DegustationPerPage)
                .OrderByDescending(d => d.Id)
                .ProjectTo<DegustationsServiceViewModel>(mapper.ConfigurationProvider)
                .ToList();

           

            query.Degustations = degustations;
            query.TotalDegustation = totalDeg;

            return View(query);
        }
    }
}
