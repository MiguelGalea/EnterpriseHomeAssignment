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
    public class OrdersService: IOrderService
    {
        private IMapper _mapper;
        private IOrdersRepository _ordersRepo;
        private ICartsRepository _cartsRepo;

        public OrdersService(IOrdersRepository ordersRepository, ICartsRepository cartsRepository , IMapper mapper)
        {
            _mapper = mapper;
            _ordersRepo = ordersRepository;
            _cartsRepo = cartsRepository;
        }        

        public void Checkout(string email)
        {
            //1. Get a list of products that have been added to the cart for the given email (from the db)
            _cartsRepo.GetCart(email);

            //2. loop within the list of products to check qty from the stock

            //3. Create order
            Order order = new Order();
            order.DateTime = DateTime.Now;
            order.UserEmail = email;
            order.Id = Guid.NewGuid();

            //3.1. Call the Addorder from inside the IOrdersRepository (this can be merged with step 3)
            _ordersRepo.AddOrder(order);

            //4. loop with the list of products and create an OrderDetail Instance for each of the products
            IList<CartViewModel> userCart = _cartsRepo.GetCart(email).ProjectTo<CartViewModel>(_mapper.ConfigurationProvider).ToList<CartViewModel>();

            foreach (var c in userCart)
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.OrderFK = order.Id;
                orderDetail.ProductFK = c.Product.Id;
                orderDetail.Quantity = c.Quantity;
                orderDetail.Price = c.Product.Price;

                _ordersRepo.AddOrderDetail(orderDetail);
            }

            foreach(var c in userCart)
            {
                var deleteCart = _cartsRepo.GetCartProduct(c.Id);

                _cartsRepo.DeleteCartProduct(deleteCart);
            }
        }
    }
}
