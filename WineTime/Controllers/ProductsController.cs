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

        [HttpPost]
        public IActionResult Add(AddProductFormModel product)
        {
            if (!data.Categories.Any(c => c.Id == product.CategoryId))
            {
                ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }

            if (!data.Manufactures.Any(c => c.Id == product.ManufactureId))
            {
                ModelState.AddModelError(nameof(product.ManufactureId), "Manufacture does not exist.");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = GetProductCategories();
                product.Manufactures = GetProductManufactures();
                return View(product);
            }

            var convertDecimal = Decimal.Parse(product.Price,
           NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

            var productData = new Product
            {

                Name = product.Name,
                Price = convertDecimal,
                ImageUrl = product.ImageUrl,
                Description = product.Description,
                CategoryId = product.CategoryId,
                YearOfManufacture = product.YearOfManufacture,
                ManufactureId = product.ManufactureId,
                Sort = product.Sort
            };

            data.Products.Add(productData);
            data.SaveChanges();

            return RedirectToAction("/");
        }


        private IEnumerable<ProductCategoryViewModel> GetProductCategories()
           => this.data
            .Categories
            .Select(c => new ProductCategoryViewModel
            {
                Id= c.Id,
                Name= c.Name,
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
