namespace WineTime.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Globalization;
    using WineTime.Infrastructure.Data;
    using WineTime.Models;
    using WineTime.Models.Products;

    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext data;

        public ProductsController(ApplicationDbContext _data) => data = _data;

        public IActionResult Add() => View(new AddProductFormModel
        {
            Categories = GetProductCategories(),
            Manufactures = GetProductManufactures()
        });


        public IActionResult All(string searchTerm, string category)
        {
            var productQuery = data.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                productQuery = productQuery.Where(x => x.Category.Name == category);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productQuery = productQuery.Where(p =>
                    p.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var products = data
                .Products
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

            return View(new AllProductQueryModel
            {
                Names = productNames,
                Categories = productCategories,
                Products = products,
                SearchTerm = searchTerm
            });
        }

        [HttpPost]
        public IActionResult Add(AddProductFormModel product)
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
                product.Categories = GetProductCategories();
                product.Manufactures = GetProductManufactures();

                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var e in errors)
                {
                    Console.WriteLine(e.ErrorMessage);
                }
                return View(product);
            }


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

            Console.WriteLine(productData);
            data.Products.Add(productData);
            data.SaveChanges();

            return RedirectToAction(nameof(All));
        }


        private IEnumerable<ProductCategoryViewModel> GetProductCategories()
           => this.data
            .Categories
            .Select(c => new ProductCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
            })
            .ToList();

        private IEnumerable<ProductManufactureViewModel> GetProductManufactures()
           => this.data
            .Manufactures
            .Select(c => new ProductManufactureViewModel
            {
                Id = c.Id,
                ManufactureName = c.ManufactureName,
                Region = c.Region.Country
            })
            .ToList();
    }
}
