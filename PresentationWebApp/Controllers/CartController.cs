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
    public class CartController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ICartsService _cartsService;
        private IHostingEnvironment _env;

        public CartController(IProductsService productsService, ICartsService cartsService, IHostingEnvironment env)
        {
            _productsService = productsService;
            _cartsService = cartsService;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
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
    }
}
