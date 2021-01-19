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

        public void DeleteCartProduct(Guid id, string email)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Cart> GetCart(string email)
        {
            throw new NotImplementedException();
        }

        public Product GetCartProducts(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
