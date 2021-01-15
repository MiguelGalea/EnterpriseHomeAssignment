using ShoppingCart.Application.Interfaces;
using ShoppingCart.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.Services
{
    class OrdersService : IOrdersService
    {

        /*
         approach - storing items in cart table in db
         a) user must be logged in
         b) in the checkout method you need to fetch list of cart items from db

        approach - storing items in cart in the controller
        a) in the checkout method you need to pass the list from the Controller to the checkout

        approach - storing items in cart in a cookie
        a) user may not be logged in
        b) in the checkout method you need to pass the list from the Controller after you have read the list from the cookie and parsed
           it to the checkout
         
         */

        public void Checkout(string email)
        {
            //1. Get a list of products that have been added to the cart for the given email (from the db, from cookie, from memory)

            //2. loop within the list of products to check qty from the stock
            // if you find a product with qty > stock - throw new Exception("Not enough stock");   OR
            // if you find a product with qty > stock - return false; 

            //3. Create Order
            Guid orderId = Guid.NewGuid();
            Order o = new Order();
            o.Id = orderId;
            //continue setting up the other properties
             //3.1 Call the AddOrder from inside the IOrdersRepository 

            //4. loop with the list of products and create and OrderDetail for each of the products
            // start of loop
            OrderDetail detail = new OrderDetail();
            detail.OrderFK = orderId;
            //detail.ProductFK = 
            //4.1 deduct qty from stock

            //continue setting up the other properties

            //end of loop

            //4.2. Call the AddOrderDetail from inside the IOrdersRepository (this can be merged with step 4)




        }
    }
}
