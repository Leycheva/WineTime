namespace WineTime.Models
{
    using System.ComponentModel.DataAnnotations;
    using WineTime.Models.Products;

    public class AddProductFormModel
    {
        public string Name { get; init; }

        [Display(Name = "Image")]
        public string ImageUrl { get; init; }

        [Display(Name = "Year")]
        public string YearOfManufacture { get; init; }

        public string Price { get; init; }

        public string Type { get; init; }

        public string Description { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<ProductCategoryViewModel> Categories { get; set; }
        

        [Display(Name = "Manufacture")]
        public int ManufactureId { get; init; }

        public IEnumerable<ProductManufactureViewModel> Manufactures { get; set; }
       

        [Display(Name = "Region")]
        public int RegionId { get; init; }

        public IEnumerable<ProductRegionViewModel> Regions { get; set; }

    }
}