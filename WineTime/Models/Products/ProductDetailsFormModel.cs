﻿namespace WineTime.Models.Products
{

    using WineTime.Core.Models;

    public class ProductDetailsFormModel : ProductFormModel
    {
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