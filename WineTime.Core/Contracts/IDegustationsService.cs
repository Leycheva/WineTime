

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

        void Update(
            int id,
            string Name,
            string Description,
            string Address,
            string dateTime,
            int seats);

        void Delete(int id);

        public DegustationsServiceViewModel Details(int id);

        IEnumerable<DegustationsServiceViewModel> All();
    }
}
