using SportsEcommerce.Domain.Abstract;
using SportsEcommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsEcommerce.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository prodtRepo;

        public AdminController(IProductRepository prodtRepo)
        {
            this.prodtRepo = prodtRepo;
        }

        public ViewResult Index()
        {
            return View(prodtRepo.Products);
        }

        public ViewResult Edit(int productId)
        {
            Product product = prodtRepo.Products.FirstOrDefault(p => p.ProductID == productId);

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
           if (ModelState.IsValid)
            {
                prodtRepo.SaveProduct(product);
                // TempData is deleted at the end of http request
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
           // there is something wrong with the data values
            else
            {
                return View(product);
            }

        }

        public ViewResult Create()
        {
            return View("Edit", new Product());

        }

        [HttpPost]
        public ActionResult DeleteProduct(int productId)
        {
            Product deletedProduct = prodtRepo.DeleteProduct(productId);

            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} Was deleted", deletedProduct.Name);

            }

            return RedirectToAction("Index");

        }
    }
}