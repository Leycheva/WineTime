namespace WineTime.Core.Services
{
    using WineTime.Core.Contracts;
    using WineTime.Infrastructure.Data;

    public class RegionsService : IRegionsService
    {
        private readonly ApplicationDbContext data;

        public RegionsService(ApplicationDbContext _data) => data = _data;

        public int Create(string country)
        {
            var regionData = new Region
            {
                Country = country
            };

            data.Regions.Add(regionData);
            data.SaveChanges();

            return regionData.Id;
        }
    }
}
