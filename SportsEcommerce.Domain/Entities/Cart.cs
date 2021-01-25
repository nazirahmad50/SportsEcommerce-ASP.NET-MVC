using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsEcommerce.Domain.Entities
{
    public class Cart
    {
        private List<CartItem> cartItems = new List<CartItem>();

        /// <summary>
        /// get all the cart iteme
        /// </summary>
        public IEnumerable<CartItem> CartItems => cartItems;

        /// <summary>
        /// Add item to cart
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        public void AddItem(Product product, int quantity)
        {
            // check if the item in the cart matches the newly added item
            CartItem cartItem = cartItems.Where(c => c.Product.ProductID == product.ProductID).FirstOrDefault();

            if (cartItem == null)
                cartItems.Add(new CartItem { Product = product, Quantity = quantity });
            else
                cartItem.Quantity += quantity;
        }

        /// <summary>
        /// Remove item from cart
        /// </summary>
        /// <param name="product"></param>
        public void RemoveCartItem(Product product)
        {
            cartItems.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }

        /// <summary>
        /// Total price of the items in cart
        /// </summary>
        /// <returns></returns>
        public decimal ComputeTotalValue()
        {
            return cartItems.Sum(s => s.Product.Price * s.Quantity);
        }

        /// <summary>
        /// Clear the all the items from cart
        /// </summary>
        public void Clear()
        {
            cartItems.Clear();
        }

       
    }
}
