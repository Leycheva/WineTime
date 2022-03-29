namespace WineTime.Areas.Admin.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using WineTime.Core.Models;
    using WineTime.Infrastructure.Data;

    public class ProductFormModel
    {

        public int? Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2,
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

        [Range(1, 1000)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; init; }

        [Required]
        public Sort Sort { get; init; }

        [Required]
        [StringLength(1000, MinimumLength = 10,
             ErrorMessage = "The Description field must be a text with minimun lenght 10 and maximum length 1000!")]
        public string Description { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }
        public IEnumerable<ProductCategoryServiceModel>? Categories { get; set; }

        [Display(Name = "Manufacture")]
        public int ManufactureId { get; init; }
        public IEnumerable<ProductManufactureServiceModel>? Manufactures { get; set; }

        public string Category
        {
            get => Categories?.FirstOrDefault(x => x.Id == CategoryId)?.Name ?? "";
        }
        public string Manufacture
        {
            get => Manufactures?.FirstOrDefault(x => x.Id == ManufactureId)?.ManufactureName ?? "";
        }

    }
}
