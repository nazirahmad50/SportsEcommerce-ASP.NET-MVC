using SportsEcommerce.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsEcommerce.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository prodRepo;

        public NavController(IProductRepository prodRepo)
        {
            this.prodRepo = prodRepo;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = prodRepo.Products
                                           .Select(x => x.Category)
                                           .Distinct()
                                           .OrderBy(x => x);

            return PartialView(categories);


        }
    }
}