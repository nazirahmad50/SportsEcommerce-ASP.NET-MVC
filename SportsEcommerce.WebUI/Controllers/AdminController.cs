using SportsEcommerce.Domain.Abstract;
using SportsEcommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsEcommerce.WebUI.Controllers
{
    [Authorize]
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <param name="image">Pass the uploaded file data</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
           if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }

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