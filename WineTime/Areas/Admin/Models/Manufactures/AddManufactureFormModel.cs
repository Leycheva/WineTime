namespace WineTime.Areas.Admin.Models
{
    using System.ComponentModel.DataAnnotations;
    using WineTime.Core.Models.Manufactures;

    public class AddManufactureFormModel
    {
        [Required]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "The Name field must be a text with minimun lenght 2 and maximum length 50!")]
        [Display(Name ="Name")]
        public string ManufactureName { get; set; }

        [Required]
        [Url]
        [StringLength(1000)]
        [Display(Name = "Logo")]
        public string ImageUrl { get; set; }


        [Display(Name = "Region")]
        public int RegionId { get; init; }

        public IEnumerable<ManufactureRegionServiceModel>? Regions { get; set; }
    }
}
