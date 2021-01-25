using SportsEcommerce.Domain.Abstract;
using SportsEcommerce.Domain.Entities;
using SportsEcommerce.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsEcommerce.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository prodRepo;
        public CartController(IProductRepository prodRepo)
        {
            this.prodRepo = prodRepo;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

       public RedirectToRouteResult AddToCart(int productId, string returnUrl)
        {
            Product product = prodRepo.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
                GetCart().AddItem(product, 1);

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = prodRepo.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
                GetCart().RemoveCartItem(product); 

            return RedirectToAction("Index", new { returnUrl });
        }

        /// <summary>
        /// Get cart items from session
        /// </summary>
        /// <returns></returns>
        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            return cart;
        }
    }
}