namespace WineTime.Areas.Admin.Models
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using System.Globalization;
    using WineTime.Areas.Admin.Controllers;
    using WineTime.Core.Constants;
    using WineTime.Infrastructure.Data;

    public class ProductsController : AdminController
    {
        private readonly ApplicationDbContext data;
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductsController(
            ApplicationDbContext _data,
            IProductService _productService,
            IMapper _mapper)
        {
            data = _data;
            productService = _productService;
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
                product.Price.ToString(),
                product.ImageUrl,
                product.Description,
                product.CategoryId,
                product.YearOfManufacture,
                product.ManufactureId,
                product.Sort
               );


            return RedirectToAction("All");
        }

        //public void ConvertPrice(string price)
        //{
        //    var conPrice = Decimal.Parse(price,
        //NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
        //}


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
                product.Price.ToString(),
                product.ImageUrl,
                product.Description,
                product.CategoryId,
                product.YearOfManufacture,
                product.ManufactureId,
                product.Sort
               );


            return RedirectToAction("All");
        }


        public IActionResult Delete(int id)
        {

            productService.Delete(id);

            return RedirectToAction("All");

        }
    }
}

    

