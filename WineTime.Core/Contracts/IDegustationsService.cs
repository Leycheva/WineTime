namespace WineTime.Core.Contracts
{
    using WineTime.Core.Models.Degustations;

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
