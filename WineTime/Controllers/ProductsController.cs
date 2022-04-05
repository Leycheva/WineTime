namespace WineTime.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Areas.Admin.Models;
    using WineTime.Core.Constants;
    using WineTime.Infrastructure.Data;

    public class ProductsController : BaseController
    {
        private readonly ApplicationDbContext data;
        private readonly IProductsService productService;
        private readonly IMapper mapper;

        public ProductsController(
            ApplicationDbContext _data,
            IProductsService _productService,
            IMapper _mapper)
        {
            data = _data;
            productService = _productService;
            mapper = _mapper;
        }

        public IActionResult All([FromQuery] AllProductQueryModel query)
        {
            var productQuery = data.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Category))
            {
                productQuery = productQuery.Where(x => x.Category.Name == query.Category);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                productQuery = productQuery.Where(p =>
                    p.Name.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            productQuery = query.Sorting switch
            {
                ProductSorting.Manufacture => productQuery.OrderBy(p => p.Manufacture.ManufactureName),
                ProductSorting.Year => productQuery.OrderByDescending(p => p.YearOfManufacture),
                ProductSorting.Price or _ => productQuery.OrderByDescending(p => p.Price)
            };

            var totalProducts = productQuery.Count();

            var products = productQuery
                .Skip((query.CurrentPage - 1) * AllProductQueryModel.ProductPerPage)
                .Take(AllProductQueryModel.ProductPerPage)
                .OrderByDescending(p => p.Id)
                .ProjectTo<ProductListingViewModel>(mapper.ConfigurationProvider)
                .ToList();


            var productNames = data
                .Products
                .Select(p => p.Name)
                .Distinct()
                .ToList();

            var productCategories = data
                .Products
                .Select(p => p.Category.Name)
                .Distinct()
                .ToList();

            query.Products = products;
            query.TotalProducts = totalProducts;
            query.Categories = productCategories;

            return View(query);
        }

        public IActionResult Details(int id)
        {
            var product = productService.Details(id);

            var productForm = mapper.Map<ProductFormModel>(product);
            productForm.Manufactures = productService.GetProductManufactures();
            productForm.Categories = productService.GetProductCategories();

            return View(productForm);

        }

    }
}