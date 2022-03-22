namespace WineTime.Core.Services
{
    using System.Globalization;
    using WineTime.Core.Contracts;
    using WineTime.Core.Models;
    using WineTime.Infrastructure.Data;

    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext data;

        public ProductService(ApplicationDbContext _data) => data = _data;

        public void Add(AddProductsServiceModel product)
        {
            var convertDecimal = Decimal.Parse(product.Price,
            NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

            var manufacture = GetProductManufactures().FirstOrDefault(m => m.Id == product.ManufactureId);
            var region = data.Regions.FirstOrDefault(r => r.Country == manufacture.Region);

            var productData = new Product
            {
                Name = product.Name,
                Price = convertDecimal,
                ImageUrl = product.ImageUrl,
                Description = product.Description,
                CategoryId = product.CategoryId,
                YearOfManufacture = product.YearOfManufacture,
                ManufactureId = product.ManufactureId,
                Sort = product.Sort,
                RegionId = region.Id
            };

            data.Products.Add(productData);
            data.SaveChanges();

        }

        public IEnumerable<ProductCategoryServiceModel> GetProductCategories()
         => this.data
          .Categories
          .Select(c => new ProductCategoryServiceModel
          {
              Id = c.Id,
              Name = c.Name,
          })
          .ToList();

        public IEnumerable<ProductManufactureServiceModel> GetProductManufactures()
           => this.data
            .Manufactures
            .Select(c => new ProductManufactureServiceModel
            {
                Id = c.Id,
                ManufactureName = c.ManufactureName,
                Region = c.Region.Country
            })
            .ToList();
    }

}
