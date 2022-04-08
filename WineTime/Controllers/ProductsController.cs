namespace WineTime.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Areas.Admin.Models;
    using WineTime.Core.Constants;

    public class ProductsController : BaseController
    {
        private readonly IProductsService productService;
        private readonly IMapper mapper;

        public ProductsController(
            IProductsService _productService,
            IMapper _mapper)
        {
            productService = _productService;
            mapper = _mapper;
        }

        public IActionResult All([FromQuery] AllProductQueryModel query)
        {
            var queryResult = productService.All(
                query.Category,
                query.SearchTerm,
                query.Name,
                query.Sorting,
                AllProductQueryModel.ProductPerPage,
                query.CurrentPage);

            var productCategories = productService.GetProductCategories().ToList();

            query.Products = queryResult.Products;
            query.TotalProducts = queryResult.TotalProducts;
            query.Categories = productCategories.Select(x => x.Name);

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