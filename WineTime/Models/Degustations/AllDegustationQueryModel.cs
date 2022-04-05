namespace WineTime.Models.Degustations
{
    using WineTime.Core.Models.Degustations;

    public class AllDegustationQueryModel
    {
        public const int DegustationPerPage = 3;

        public string Degustation { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int PageCount { get; set; }

        public int TotalDegustation { get; set; } = 1;

        public ICollection<DegustationsServiceViewModel> Degustations { get; set; }
    }
}
