using AutoMapper;
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

        public ProductViewModel GetCartProducts(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<CartViewModel> GetCart(string email)
        {
            throw new NotImplementedException();
        }

        public void DeleteCartProduct(Guid id, string email)
        {
            throw new NotImplementedException();
        }
    }
}
