namespace WineTime.Core.Services
{
    using System.Globalization;
    using WineTime.Core.Constants;
    using WineTime.Core.Models;
    using WineTime.Infrastructure.Data;

    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext data;

        public ProductService(ApplicationDbContext _data) => data = _data;

        public bool CategoryExists(int categoryId)
           => data
            .Categories
            .Any(p => p.Id == categoryId);

        
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

        public bool ManufactureExists(int manufactureId)
             => data
            .Manufactures
            .Any(p => p.Id == manufactureId);

        public int Create(string name, string price, string imageUrl, string description, 
            int categoryId, string yearOfManufacture, int manufactureId, Sort sort)
        {
            var convertPrice = Decimal.Parse(price,
            NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

            var productData = new Product
            {
                Name = name,
                Price = convertPrice,
                ImageUrl = imageUrl,
                Description = description,
                CategoryId = categoryId,
                YearOfManufacture = yearOfManufacture,
                ManufactureId = manufactureId,
                Sort = sort
            };

            data.Products.Add(productData);
            data.SaveChanges();

            return productData.Id;
        }

        public void Update(int id, string name, string price, string imageUrl, string description,
            int categoryId, string yearOfManufacture, int manufactureId, Sort sort)
        {
            var convertPrice = Decimal.Parse(price,
            NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

            var product = data.Products.FirstOrDefault(p => p.Id == id);
            product.Name = name;
            product.Price = convertPrice;
            product.ImageUrl = imageUrl;
            product.Description = description;
            product.CategoryId = categoryId;
            product.YearOfManufacture = yearOfManufacture;
            product.ManufactureId = manufactureId;
            product.Sort = sort;

            data.SaveChanges();

        }

        public ProductsServiceModel? Details(int id)
            => this.data
            .Products
            .Where(p => p.Id == id)
            .Select(p => new ProductsServiceModel
            {
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Description = p.Description,
                CategoryId = p.CategoryId,
                YearOfManufacture = p.YearOfManufacture,
                Sort = p.Sort,
                ManufactureId = p.ManufactureId
            })
            .FirstOrDefault();

        public IEnumerable<ProductRegionServiceModel> GetProductRegion()
         => this.data
            .Regions
            .Select(c => new ProductRegionServiceModel
            {
                Id = c.Id,
                Country = c.Country
            })
            .ToList();
    }

}
