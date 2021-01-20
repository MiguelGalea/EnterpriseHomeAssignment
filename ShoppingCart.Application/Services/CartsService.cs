using AutoMapper;
using AutoMapper.QueryableExtensions;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Application.ViewModels;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class CartsService : ICartsService
    {
        private ICartsRepository _cartsRepo;
        private IMapper _mapper;

        public CartsService(ICartsRepository cartsRepository, IMapper mapper)
        {
            _cartsRepo = cartsRepository;
            _mapper = mapper;
        }

        public void AddCartProduct(CartViewModel cart)
        {
            var addCart = _mapper.Map<Cart>(cart);

            addCart.Product_FK = addCart.Product.ID;
            addCart.Product = null;

            _cartsRepo.AddCartProduct(addCart);
        }

        public CartViewModel GetCartProduct(int id)
        {
            var cartProducts = _cartsRepo.GetCartProduct(id);
            var myCart = _mapper.Map<CartViewModel>(cartProducts);

            return myCart;
        }

        public IQueryable<CartViewModel> GetCart(string email)
        {
            var myCart = _cartsRepo.GetCart(email).ProjectTo<CartViewModel>(_mapper.ConfigurationProvider);

            return myCart;
        }

        public void DeleteCartProduct(int id)
        {
            var deleteProd = _cartsRepo.GetCartProduct(id);

            if (deleteProd != null)
            {
                _cartsRepo.DeleteCartProduct(deleteProd);
            }
        }
    }
}
