namespace WineTime.Core.Constants
{ 
    using WineTime.Core.Models;
    using WineTime.Infrastructure.Data;

    public interface IProductsService
    {
        ProductQueryServiceModel All(
            string category,
            string searchTerm,
            string name,
            ProductSorting sorting,
            int productPerPage = int.MaxValue,
            int currentPage = 1);
            
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

        void Delete(int id);

        bool CategoryExists(int categoryId);


        public ProductsServiceModel Details(int id);

        IEnumerable<ProductCategoryServiceModel> GetProductCategories();


        IEnumerable<ProductManufactureServiceModel> GetProductManufactures();

        IEnumerable<ProductRegionServiceModel> GetProductRegion();



    }
}
