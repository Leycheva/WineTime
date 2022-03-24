namespace WineTime.Core.Constants
{ 
    using WineTime.Core.Models;
    using WineTime.Infrastructure.Data;

    public interface IProductService
    {
        //void Add(ProductsServiceModel product);

        int Create(string name,
                   string price,
                   string imageUrl,
                   string description,
                   int categoryId,
                   string yearOfManufacture,
                   int manufactureId,
                   Sort Sort);

        void Update(int id,
                       string name,
                       string price,
                       string imageUrl,
                       string description,
                       int categoryId,
                       string yearOfManufacture,
                       int manufactureId,
                       Sort Sort);

        bool CategoryExists(int categoryId);

        bool ManufactureExists(int manufactureId);

        public ProductsServiceModel Details(int id);

        IEnumerable<ProductCategoryServiceModel> GetProductCategories();

        IEnumerable<ProductManufactureServiceModel> GetProductManufactures();



    }
}
