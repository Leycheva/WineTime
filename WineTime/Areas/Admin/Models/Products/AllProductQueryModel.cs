namespace WineTime.Areas.Admin.Models
{
    using System.ComponentModel.DataAnnotations;
    using WineTime.Core.Models;

    public class AllProductQueryModel
    {

        public const int ProductPerPage = 6;

        public string Category { get; set; }

        
        [Display(Name="Search by text")]
        public string SearchTerm { get; set; }

        public string Name { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int PageCount { get; set; }

        public int TotalProducts { get; set; } = 1;

        public ProductSorting Sorting { get; set; }

        public IEnumerable<string> Names { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<ProductListingServiceModel> Products { get; set; }

    }
}
