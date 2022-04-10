namespace WineTime.Core.Contracts
{
    public interface IRegionsService
    {
        int Create(string country);

        bool RegionExists(int regionId);
    }
}
