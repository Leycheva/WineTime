using WineTime.Core.Models.Manufactures;

namespace WineTime.Core.Contracts
{
    public interface IManufacturesService
    {
        int Create(string manufactureName,
            string imageUrl,
            int regionId);

        IEnumerable<ManufactureRegionServiceModel> GetManufactureRegions();

        bool RegionExists(int regionId);
    }
}
