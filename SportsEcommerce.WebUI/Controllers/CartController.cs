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

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

       public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = prodRepo.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
                cart.AddItem(product, 1);

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId,string returnUrl)
        {
            Product product = prodRepo.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
                cart.RemoveCartItem(product);


            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
    }
}