namespace WineTime.Core.Services
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
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
    }
}
