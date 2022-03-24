namespace WineTime.Core.Contracts
{
    using System.ComponentModel.DataAnnotations;
    using WineTime.Core.Models;
    using WineTime.Infrastructure.Data;

    public class ProductServiceModel 
    {
        public int ProductId { get; set; }

        public string Name { get; init; }

        public string ImageUrl { get; init; }

        [Display(Name = "Year")]
        public string YearOfManufacture { get; init; }

        public string Price { get; init; }

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