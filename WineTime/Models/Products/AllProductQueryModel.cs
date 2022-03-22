using System.ComponentModel.DataAnnotations;

namespace WineTime.Models.Products
{
    public class AllProductQueryModel
    {
        public string Category { get; set; }

        public IEnumerable<string> Categories { get; set; }

        [Display(Name="Search")]
        public string SearchTerm { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> Names { get; set; }

        public ProductSorting Sorting { get; set; }

        public IEnumerable<ProductListingViewModel> Products { get; set; }
    }
}
