﻿namespace WineTime.Models.Products
{
    using WineTime.Infrastructure.Data;

    public class FavoritesFormModel
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string YearOfManufacture { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Region { get; set; }

        public string Description { get; set; }

        public Sort Sort { get; set; }

    }
}
