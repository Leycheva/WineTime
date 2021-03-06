namespace WineTime.Core.Models.Degustations
{
    public class DegustationsServiceViewModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string DateTime { get; set; }

        public int Seats { get; set; }

        public int BookSeats { get; set; }

        public IEnumerable<DegustationsServiceViewModel> Degustations { get; set; }
    }
}
