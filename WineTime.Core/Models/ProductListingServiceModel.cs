namespace WineTime.Core.Models
{
    public class ProductListingServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string YearOfManufacture { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }
    }
}
