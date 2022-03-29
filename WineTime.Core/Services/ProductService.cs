namespace WineTime.Core.Services
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using System.Globalization;
    using WineTime.Core.Constants;
    using WineTime.Core.Models;
    using WineTime.Infrastructure.Data;

    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public ProductService(
            ApplicationDbContext _data,
             IMapper _mapper)
        {
            data = _data;
            mapper = _mapper;
        }

        public bool CategoryExists(int categoryId)
           => data
            .Categories
            .Any(p => p.Id == categoryId);

        
        public IEnumerable<ProductCategoryServiceModel> GetProductCategories()
         => this.data
          .Categories
          .ProjectTo<ProductCategoryServiceModel>(mapper.ConfigurationProvider)
          .ToList();

        public IEnumerable<ProductManufactureServiceModel> GetProductManufactures()
           => this.data
            .Manufactures
            .ProjectTo<ProductManufactureServiceModel>(mapper.ConfigurationProvider)
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
                Sort = sort,
                RegionId = 1,
                Region = null
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
            .ProjectTo<ProductsServiceModel>(mapper.ConfigurationProvider)
            .FirstOrDefault();

        public IEnumerable<ProductRegionServiceModel> GetProductRegion()
         => this.data
            .Regions
            .ProjectTo<ProductRegionServiceModel>(mapper.ConfigurationProvider)
            .ToList();

        public void Delete(int id)
        {
            var product = data.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return;
            }

            data.Products.Remove(product);
            data.SaveChanges();
        }
    }

}
