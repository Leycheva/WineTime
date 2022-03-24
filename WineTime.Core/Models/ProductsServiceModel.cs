using System.ComponentModel.DataAnnotations;
using WineTime.Infrastructure.Data;

namespace WineTime.Core.Models
{
    public class ProductsServiceModel
    {
        public int Id { get; set; }

        public string Name { get; init; }

        public string ImageUrl { get; init; }

        public string YearOfManufacture { get; init; }

        public decimal Price { get; init; }

        public Sort Sort { get; init; }

        public string Description { get; init; }


        [Display(Name = "Category")]
        public int CategoryId { get; init; }
        public IEnumerable<ProductCategoryServiceModel>? Categories { get; set; }


        [Display(Name = "Manufacture")]
        public int ManufactureId { get; init; }
        public IEnumerable<ProductManufactureServiceModel>? Manufactures { get; set; }

    }
}
