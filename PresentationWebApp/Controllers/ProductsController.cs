using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using X.PagedList;

namespace PresentationWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ICategoriesService _categoriesService;
        private IHostingEnvironment _env;

        public ProductsController(IProductsService productsService, ICategoriesService categoriesService, IHostingEnvironment env) {
            _productsService = productsService;
            _categoriesService = categoriesService;
            _env = env;
        }

        public IActionResult Index(int? page) {
            //https://www.youtube.com/watch?v=vnxN_zBisIo pagination tutorial
            var pageNum = page ?? 1; //default page 1
            int pageCap = 10; //Maximum 10 products per page

            var list = _productsService.GetProducts().ToPagedList(pageNum, pageCap);

            var categories = _categoriesService.GetCategories();
            ViewBag.Categories = categories;

            return View(list);
        }

        [HttpPost]
        public IActionResult CategoriesFilter(int category, int? page) //Using a Form, and the select list must have name attribute = category
        {

            var list = _productsService.GetProducts(category).ToPagedList();

            var categories = _categoriesService.GetCategories();
            ViewBag.Categories = categories;

            return View("Index", list);
            
        }

        [HttpPost]
        public IActionResult Search(string keyword, int? page) {
            var list = _productsService.GetProducts(keyword).ToPagedList();

            var categories = _categoriesService.GetCategories();
            ViewBag.Categories = categories;

            return View("Index", list);
        }

        public IActionResult Details(Guid id) {
            var p = _productsService.GetProduct(id);

            return View(p);
        }

        //The engine will load a page with empty fields
        [HttpGet]
        [Authorize (Roles = "Admin")] //The create method is going to be accessed only by authenticated users
        public IActionResult Create() {
            //Feth a list of categories
            var listOfCategories = _categoriesService.GetCategories();

            //We pass the categories to the page
            ViewBag.Categories = listOfCategories;

            return View();
        }
        
        //Here details input by the user will be received
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(ProductViewModel data, IFormFile f) {
            try
            {
                if (f != null) {
                    if (f.Length > 0) {
                        
                        string newFileName = Guid.NewGuid() + System.IO.Path.GetExtension(f.FileName);
                        string newFileNameWithAbsolutePath = _env.WebRootPath + @"\images\" + newFileName;
                        
                        using (var stream = System.IO.File.Create(newFileNameWithAbsolutePath)) {
                            f.CopyTo(stream);
                        }
                        data.ImageUrl = @"\images\" + newFileName;

                        _productsService.AddProduct(data);
                        TempData["feedback"] = "Product was added Successfully";
                    }                                     
                }
            }
            catch (Exception e) {
                TempData["warning"] = "Product was not added";
            }

            //We resend the list of categories since the page will reload
            var listOfCategories = _categoriesService.GetCategories();

            //We pass the categories to the page
            ViewBag.Categories = listOfCategories;

            return View(data);
        }

        [Authorize(Roles = "Admin")]    
        public IActionResult Delete(Guid id) {
            try
            {
                _productsService.DeleteProduct(id);
                TempData["feedback"] = "Product was deleted";
            }
            catch
            {
                TempData["warning"] = "Product was not deleted";
            }

            return RedirectToAction("Index");

        }

        [Authorize(Roles = "Admin")]
        public IActionResult Disable(Guid id)
        {
            try
            {
                _productsService.DisableProduct(id);
                TempData["feedback"] = "Product was disabled";
            }
            catch
            {
                TempData["warning"] = "Product was not disabled";
            }

            return RedirectToAction("Index");

        }

    }
}
