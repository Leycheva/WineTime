namespace WineTime.Core.Contracts
{
    using WineTime.Core.Models.Degustations;

    public interface IDegustationsService
    {
        DegustationsQueryServiceModel All(
           int degustationPerPage = int.MaxValue,
           int currentPage = 1);

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

        bool Book(string userId,int id);

        void Delete(int id);

        public DegustationsServiceViewModel Details(int id);

        IEnumerable<DegustationsServiceViewModel> All();
    }
}
