namespace WineTime.Core.Models.Manufactures
{
    using System.ComponentModel.DataAnnotations;

    public class ManufactureServiceModel
    {
        public int Id { get; set; }

        public string ManufactureName { get; set; }

        public string ImageUrl { get; set; }

        [Display(Name = "Region")]
        public int RegionId { get; init; }
        public IEnumerable<ManufactureRegionServiceModel>? Regions { get; set; }
    }
}
