namespace WineTime.Areas.Admin.Models
{
    using System.ComponentModel.DataAnnotations;

    public class DegustationsFormModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Degustation Name")]
        [StringLength(200, MinimumLength = 10,
             ErrorMessage = "The Name field must be a text with minimun lenght 10 and maximum length 200!")]
        public string Name { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 10,
           ErrorMessage = "The Description field must be a text with minimun lenght 10 and maximum length 500!")]
        public string Description { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8,
             ErrorMessage = "The Address field must be a text with minimun lenght 8 and maximum length 100!")]
        public string Address { get; set; }

        [Display(Name = "Date&Time")]
        [DataType(DataType.DateTime)]
        public string DateTime { get; set; }

        [Required]
        [Range(1,100)]
        [Display(Name = "Available Seats")]
        public int Seats { get; set; }

    }
}
