using ShoppingCart.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Application.Interfaces
{
    public interface ICartsService
    {
        IQueryable<CartViewModel> GetCart(string email);

        ProductViewModel GetCartProducts(Guid id);

        void AddCartProduct(CartViewModel product);

        void DeleteCartProduct(Guid id, string email);

    }
}
