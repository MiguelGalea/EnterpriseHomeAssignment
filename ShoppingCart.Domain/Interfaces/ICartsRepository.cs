using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Domain.Interfaces
{
    public interface ICartsRepository
    {
        IQueryable<Cart> GetCart(string email);

        Product GetCartProducts(Guid id);

        int AddCartProduct(Cart cart);

        void DeleteCartProduct(Guid id, string email);
    }
}
