using Moq;
using NUnit.Framework;
using SportsEcommerce.Domain.Abstract;
using SportsEcommerce.Domain.Entities;
using SportsEcommerce.WebUI.Controllers;
using SportsEcommerce.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsEcommerce.UnitTests
{
    public class CartTests
    {
        private Cart cart;
        private CartController controller;

        private Product p1;
        private Product p2;
        private Product p3;

        [SetUp]
        public void Init()
        {
       
             p1 = new Product { ProductID = 1, Name = "P1", Price = 10m };
             p2 = new Product { ProductID = 2, Name = "P2", Price = 50m };
             p3 = new Product { ProductID = 3, Name = "P3" };

            cart = new Cart();

            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product{ProductID = 1, Name = "P1", Category = "C1"}
            }.AsQueryable());

            controller = new CartController(mock.Object, null);
        }


        [Test]
        public void Add_new_Cart_Item()
        {
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);

            CartItem[] results = cart.CartItems.ToArray();


            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Product, p1);
            Assert.AreEqual(results[1].Product, p2);
        }

        [Test]
        public void Add_Quantity_For_Existing_Cart_Item()
        {
            cart.AddItem(p3, 2);
            cart.AddItem(p3, 10);
            cart.AddItem(p1, 2);

            CartItem[] results = cart.CartItems.OrderBy(o => o.Product.ProductID).ToArray();

            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Quantity, 2);
            Assert.AreEqual(results[1].Quantity, 12);
        }


        [Test]
        public void Can_Remove_Cart_Item()
        {
            cart.AddItem(p3, 2);
            cart.AddItem(p3, 10);
            cart.AddItem(p1, 2);
            cart.AddItem(p2, 2);

            cart.RemoveCartItem(p1);
            cart.RemoveCartItem(p3);

            Assert.AreEqual(cart.CartItems.Where(c => c.Product == p1).Count(), 0);
            Assert.AreEqual(cart.CartItems.Count(), 1);
        }


        [Test]
        public void Calculate_Cart_Total()
        {
            cart.AddItem(p1, 2);
            cart.AddItem(p2, 2);

            decimal result = cart.ComputeTotalValue();

            Assert.AreEqual(result, 120);
        }


        [Test]
        public void Can_Clear_Content()
        {
            cart.AddItem(p1, 2);
            cart.AddItem(p2, 2);
            cart.AddItem(p3, 10);

            cart.Clear();

            Assert.AreEqual(cart.CartItems.Count(), 0);
        }

        #region CartController Tests

        [Test]
        public void Can_Add_To_Cart()
        {
            controller.AddToCart(cart, 1, null);

            Assert.AreEqual(cart.CartItems.Count(), 1);
            Assert.AreEqual(cart.CartItems.ToArray()[0].Product.ProductID, 1);

        }

        [Test]
        public void Adding_Product_To_Cart_Goes_To_Cart_Screen()
        {
            RedirectToRouteResult result = controller.AddToCart(cart, 2, "myUrl");

            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");

        }


        [Test]
        public void Can_View_Cart_Contents()
        {
            CartController cartController = new CartController(null);

            CartIndexViewModel result = (CartIndexViewModel)cartController.Index(cart, "myUrl").Model;

            Assert.AreEqual(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");

        }

        #endregion
    }
}
