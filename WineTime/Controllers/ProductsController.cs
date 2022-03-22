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
