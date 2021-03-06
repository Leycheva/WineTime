namespace WineTime.Areas.Admin.Models
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using WineTime.Areas.Admin.Controllers;
    using WineTime.Core.Constants;
    using WineTime.Core.Contracts;

    public class ProductsController : AdminController
    {
        private readonly IProductsService productService;
        private readonly IManufacturesService manufactureService;
        private readonly IMapper mapper;

        public ProductsController(
            IProductsService _productService,
            IManufacturesService _manufactureService,
            IMapper _mapper)
        {
            productService = _productService;
            manufactureService = _manufactureService;
            mapper = _mapper;
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

            if (!manufactureService.ManufactureExists(product.ManufactureId))
            {
                ModelState.AddModelError(nameof(product.ManufactureId), "Manufacture does not exist.");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = productService.GetProductCategories();
                product.Manufactures = productService.GetProductManufactures();

                return View(product);
            }

                productService.Create(
                product.Name,
                product.Price.ToString(),
                product.ImageUrl,
                product.Description,
                product.CategoryId,
                product.YearOfManufacture,
                product.ManufactureId,
                product.Sort
               );

            return RedirectToAction("All", "Products", new { area = "" });
        }

        public IActionResult Edit(int id)
        {
            var product = productService.Details(id);
            var productForm = mapper.Map<ProductFormModel>(product);
            productForm.Manufactures = productService.GetProductManufactures();
            productForm.Categories = productService.GetProductCategories();

            return View(productForm);
        }

        [HttpPost]
        public IActionResult Edit(ProductFormModel product)
        {

            if (!productService.CategoryExists(product.CategoryId))
            {
                ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }

            if (!manufactureService.ManufactureExists(product.ManufactureId))
            {
                ModelState.AddModelError(nameof(product.ManufactureId), "Manufacture does not exist.");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = productService.GetProductCategories();
                product.Manufactures = productService.GetProductManufactures();

                return View(product);
            }

           productService.Update(
                product.Id ?? 0,
                product.Name,
                product.Price.ToString(),
                product.ImageUrl,
                product.Description,
                product.CategoryId,
                product.YearOfManufacture,
                product.ManufactureId,
                product.Sort
               );

            return RedirectToAction("All", "Products", new { area = "" });
        }

        public IActionResult Delete(int id)
        {
            productService.Delete(id);

            return RedirectToAction("All", "Products", new { area = "" });
        }
    }
}

    

