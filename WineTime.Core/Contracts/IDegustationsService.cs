

using WineTime.Core.Models;

namespace WineTime.Core.Contracts
{
    public interface IDegustationsService
    {
        int Create(
            string Name,
            string Description,
            string Address,
            string dateTime,
            int seats);

        void Update();

        void Delete(int id);

        IEnumerable<DegustationsServiceViewModel> All();
    }
}
