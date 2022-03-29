namespace WineTime.Areas.Admin.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AddRegionFormModel
    {
        [Required]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "The Country field must be a text with minimun lenght 2 and maximum length 50!")]
        public string Country { get; set; }
    }
}
