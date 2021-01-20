using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationWebApp.Controllers
{
    public class CartsController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ICartsService _cartsService;
        private IHostingEnvironment _env;

        public CartsController(IProductsService productsService, ICartsService cartsService, IHostingEnvironment env)
        {
            _productsService = productsService;
            _cartsService = cartsService;
            _env = env;
        }

        [Authorize(Roles = "User, Admin")]
        public IActionResult Index()
        {
            var list = _cartsService.GetCart(User.Identity.Name);

            return View(list);
        }

        [HttpPost]
        public IActionResult AddToCart()
        {
            CartViewModel cart = new CartViewModel();
            int quantity = int.Parse(Request.Form["quantity"]);
            string productId = Request.Form["Id"];
            string email = User.Identity.Name;

            cart.Email = email;
            cart.Quantity = quantity;
            cart.Product = _productsService.GetProduct(Guid.Parse(productId));

            _cartsService.AddCartProduct(cart);

            return RedirectToAction("Index", "Products");
        }

        public IActionResult GetCart(string email)
        {
            var list = _cartsService.GetCart(email);

            return RedirectToAction("Index", list);

        }

        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public IActionResult DeleteFromCart(int id)
        {
            try
            {
                _cartsService.DeleteCartProduct(id);
                TempData["feedback"] = "Product was removed";
            }
            catch (Exception ex)
            {
                //Log your error
                TempData["warning"] = "Product was not removed";
            }

            return RedirectToAction("Index");

        }
    }
}
