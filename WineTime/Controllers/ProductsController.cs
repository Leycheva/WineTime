namespace WineTime.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Core.Contracts;
    using WineTime.Core.Models;
    using WineTime.Infrastructure.Data;
    using WineTime.Models.Products;

    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext data;

        private readonly IProductService productService;

        public ProductsController(
            ApplicationDbContext _data, 
            IProductService _productService)
        {
            data = _data;
            productService = _productService;
        }

        public IActionResult All([FromQuery]AllProductQueryModel query)
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
                .Select(p => new ProductListingViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.Name,
                    Price = p.Price,
                    YearOfManufacture = p.YearOfManufacture

                })
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

        public IActionResult Add() => View(new AddProductsServiceModel
        {
            Categories = productService.GetProductCategories(),
            Manufactures = productService.GetProductManufactures()
        });


        [HttpPost]
        public IActionResult Add(AddProductsServiceModel product)
        {

            if (!data.Categories.Any(p => p.Id == product.CategoryId))
            {
                ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }

            if (!data.Manufactures.Any(p => p.Id == product.ManufactureId))
            {
                ModelState.AddModelError(nameof(product.ManufactureId), "Manufacture does not exist.");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = productService.GetProductCategories();
                product.Manufactures = productService.GetProductManufactures();

                return View(product);
            }

            productService.Add(product);

            return RedirectToAction(nameof(All));
        }

    }
}
