namespace WineTime.Core.Services
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Globalization;
    using WineTime.Core.Contracts;
    using WineTime.Core.Models.Degustations;
    using WineTime.Infrastructure.Data;

    public class DegustationsService : IDegustationsService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public DegustationsService(
            ApplicationDbContext _data,
             IMapper _mapper)
        {
            data = _data;
            mapper = _mapper;
        }

        public IEnumerable<DegustationsServiceViewModel> All()
        {
            return data
                .Degustations
                .OrderByDescending(x => x.DateTime)
                .ProjectTo<DegustationsServiceViewModel>(mapper.ConfigurationProvider)
                .ToList();
        }

        public DegustationsQueryServiceModel All(int degustationPerPage = int.MaxValue, int currentPage = 1)
        {
            var degQuery = data.Degustations.AsQueryable();
            var totalDeg = degQuery.Count();

            var degustations = GetDegustation(degQuery
               .Skip((currentPage - 1) * degustationPerPage)
               .Take(degustationPerPage));

            return new DegustationsQueryServiceModel
            {
                TotalDegustations = totalDeg,
                CurrentPage = currentPage,
                DegustationPerPage = degustationPerPage,
                Degustations = degustations
            };
        }

        private IEnumerable<DegustationsServiceViewModel> GetDegustation(IQueryable<Degustation> degQuery)
           => degQuery
           .ProjectTo<DegustationsServiceViewModel>(mapper.ConfigurationProvider)
           .ToList();

        public int Create(string Name, string Description, string Address, string dateTime, int seats)
        {
            DateTime date;
            DateTime.TryParseExact(
                dateTime,
                "yyyy-MM-ddTHH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date);

            var degData = new Degustation
            {
                Name = Name,
                Description = Description,
                Address = Address,
                DateTime = date,
                Seats = seats
            };

            data.Degustations.Add(degData);
            data.SaveChanges();

            return degData.Id;
        }

        public void Delete(int id)
        {
            var degustation = data.Degustations.FirstOrDefault(p => p.Id == id);

            if (degustation == null)
            {
                return;
            }

            data.Degustations.Remove(degustation);
            data.SaveChanges();
        }

        public DegustationsServiceViewModel? Details(int id)
            => data
            .Degustations
            .Where(d => d.Id == id)
            .ProjectTo<DegustationsServiceViewModel>(mapper.ConfigurationProvider)
            .FirstOrDefault();

        public void Update(
            int id,
            string Name,
            string Description,
            string Address,
            string dateTime,
            int seats)
        {
            DateTime date;
            DateTime.TryParseExact(
                dateTime,
                "yyyy-MM-ddTHH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date);

            var degustation = data.Degustations.FirstOrDefault(d => d.Id == id);
            degustation.Name = Name;
            degustation.Description = Description;
            degustation.Address = Address;
            degustation.DateTime = date;
            degustation.Seats = seats;

            data.SaveChanges();
        }

        public bool Book(string userId,int id)
        {
            var degustation = data.Degustations.Include(y => y.Users).FirstOrDefault(d => d.Id == id);

            if (degustation == null)
            {
                return false;
            }
            if (!data.Users.Any(x => x.Id == userId))
            {
                return false;
            }
            if (degustation.Users.Any(x => x.UserId == userId))
            {
                return false;
            }

            var userDegustation = new UserDegustation
            {
                UserId = userId,
                DegustationId = id
            };

            degustation.Users.Add(userDegustation);
            data.SaveChanges();

            return true;
        }
    }
}
