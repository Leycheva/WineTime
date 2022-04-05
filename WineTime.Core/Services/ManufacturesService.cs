namespace WineTime.Core.Services
{
    using System.Collections.Generic;
    using WineTime.Core.Contracts;
    using WineTime.Core.Models.Manufactures;
    using WineTime.Infrastructure.Data;

    public class ManufacturesService : IManufacturesService
    {
        private readonly ApplicationDbContext data;

        public ManufacturesService(ApplicationDbContext _data) => data = _data;
        
        public int Create(string manufactureName, string imageUrl, int regionId)
        {
            var manufactureData = new Manufacture
            {
                ManufactureName = manufactureName,
                ImageUrl = imageUrl,
                RegionId = regionId,
            };

            data.Manufactures.Add(manufactureData);
            data.SaveChanges();

            return manufactureData.Id;
        }

        public IEnumerable<ManufactureRegionServiceModel> GetManufactureRegions()
                => data
                   .Regions
                   .Select(c => new ManufactureRegionServiceModel
                   {
                       Id = c.Id,
                       Country = c.Country
                   })
                   .ToList();

        public bool RegionExists(int regionId)
            => data.
            Regions.
            Any(r => r.Id == regionId);


    }
}
