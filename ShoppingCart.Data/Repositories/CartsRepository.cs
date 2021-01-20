using ShoppingCart.Data.Context;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Data.Repositories
{
    public class CartsRepository :ICartsRepository
    {
        ShoppingCartDbContext _context;
        public CartsRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }
        public int AddCartProduct(Cart cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();

            return cart.Id;
        }

        public void DeleteCartProduct(Cart c)
        {
            _context.Carts.Remove(c);
            _context.SaveChanges();
        }

        public IQueryable<Cart> GetCart(string email)
        {
            return _context.Carts;
        }

        public Cart GetCartProduct(int id)
        {
            return _context.Carts.SingleOrDefault(x => x.Id == id);
        }
    }
}
