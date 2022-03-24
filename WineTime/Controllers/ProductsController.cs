namespace WineTime.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Globalization;
    using WineTime.Core.Constants;
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

        public IActionResult Add() => View(new ProductFormModel
        {
            Categories = productService.GetProductCategories(),
            Manufactures = productService.GetProductManufactures()
        });


        [HttpPost]
        public IActionResult Add(ProductFormModel product)
        {

            if (!productService.CategoryExists(product.CategoryId))
            {
                ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }

            if (!productService.ManufactureExists(product.ManufactureId))
            {
                ModelState.AddModelError(nameof(product.ManufactureId), "Manufacture does not exist.");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = productService.GetProductCategories();
                product.Manufactures = productService.GetProductManufactures();

                return View(product);
            }


            this.productService.Create(
                product.Name,
                product.Price,
                product.ImageUrl,
                product.Description,
                product.CategoryId,
                product.YearOfManufacture,
                product.ManufactureId,
                product.Sort
               );
                
                
            return RedirectToAction(nameof(All));
        }

        public void ConvertPrice(string price)
        {
            var conPrice = Decimal.Parse(price,
        NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
        }


        public IActionResult Edit(int id)
        {
            //Do some checks after rolles are created!!

            var product = productService.Details(id);

            return View(new ProductFormModel
            {
                Id = id,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Price = product.Price.ToString("0.00"),
                Description = product.Description,
                CategoryId = product.CategoryId,
                ManufactureId = product.ManufactureId,
                YearOfManufacture = product.YearOfManufacture,  
                Sort = product.Sort,
                Manufactures = productService.GetProductManufactures(), 
                Categories = productService.GetProductCategories()
            });
        }

        [HttpPost]
        public IActionResult Edit(ProductFormModel product)
        {
            
            if (!productService.CategoryExists(product.CategoryId))
            {
                ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }

            if (!productService.ManufactureExists(product.ManufactureId))
            {
                ModelState.AddModelError(nameof(product.ManufactureId), "Manufacture does not exist.");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = productService.GetProductCategories();
                product.Manufactures = productService.GetProductManufactures();

                return View(product);
            }

            

            this.productService.Update(
                product.Id ?? 0,
                product.Name,
                product.Price,
                product.ImageUrl,
                product.Description,
                product.CategoryId,
                product.YearOfManufacture,
                product.ManufactureId,
                product.Sort
               );


            return RedirectToAction(nameof(All));
        }
    }
}
