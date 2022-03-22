using WineTime.Core.Models;

namespace WineTime.Core.Contracts
{
    public interface IProductService
    {
        void Add(AddProductsServiceModel product);

       IEnumerable<ProductCategoryServiceModel> GetProductCategories();

      IEnumerable<ProductManufactureServiceModel> GetProductManufactures();

    }
}
