using System.ComponentModel.DataAnnotations;

namespace WineTime.Core.Models
{
    public class ProductQueryServiceModel
    {
        public int CurrentPage { get; set; }

        public int ProductPerPage { get; set; }

        public int TotalProducts { get; set; }

        public IEnumerable<ProductListingServiceModel> Products { get; set; }

    }
}
