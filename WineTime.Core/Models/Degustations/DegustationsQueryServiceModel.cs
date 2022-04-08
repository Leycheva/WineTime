namespace WineTime.Core.Models.Degustations
{
    public class DegustationsQueryServiceModel
    {
        public int CurrentPage { get; set; }

        public int DegustationPerPage { get; set; }

        public int TotalDegustations { get; set; }

        public IEnumerable<DegustationsServiceViewModel> Degustations { get; set; }
    }
}
