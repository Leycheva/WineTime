namespace WineTime.Models
{
    using System.ComponentModel.DataAnnotations;
    using WineTime.Infrastructure.Data;
    using WineTime.Models.Products;

    public class AddProductFormModel
    {

        [Required]
        [StringLength(50,MinimumLength = 2,
            ErrorMessage = "The Name field must be a text with minimun lenght 2 and maximum length 50!")]
        public string Name { get; init; }


        [Required]
        [Url]
        [StringLength(1000)]
        [Display(Name = "Image")]
        public string ImageUrl { get; init; }

        [Required]
        [StringLength(4)]
        [Display(Name = "Year")]
        public string YearOfManufacture { get; init; }

        [Required]
        [StringLength(50, MinimumLength = 2,
             ErrorMessage = "The Price field must be a text with minimun lenght 2 and maximum length 50!")]
        public string Price { get; init; }

        [Required]
        public Sort Sort { get; init; }

        [Required]
        [StringLength(1000, MinimumLength = 10,
             ErrorMessage = "The Description field must be a text with minimun lenght 10 and maximum length 1000!")]
        public string Description { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }
        
        public IEnumerable<ProductCategoryViewModel>? Categories { get; set; }

        [Display(Name = "Manufacture")]
        public int ManufactureId { get; init; }

        public IEnumerable<ProductManufactureViewModel>? Manufactures { get; set; }

        
        
    }
}