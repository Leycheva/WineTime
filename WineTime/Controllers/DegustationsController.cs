namespace WineTime.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Core.Contracts;
    using WineTime.Infrastructure.Data;
    using WineTime.Models.Degustations;
    using WineTime.Extensions;
    using Microsoft.EntityFrameworkCore;
    using WineTime.Core.Models.Degustations;

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

        public IActionResult Book(int id)
        {
            
            var userId = User.GetId();
            var degustation = data.Degustations.Include(y => y.Users).FirstOrDefault(d => d.Id == id);

            if (degustation.Users.Any(x => x.UserId == userId))
            {
                return View("DegError");
            }

            var userDegustation = new UserDegustation
            {
                UserId = userId,
                DegustationId = id
            };

            degustation.Users.Add(userDegustation);

            data.SaveChanges();

            return View("Book");
        }
    }
}
