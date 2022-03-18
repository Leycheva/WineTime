namespace WineTime.Controllers
{
    using Microsoft.AspNetCore.Mvc;
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
            return View();
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
