using SportsEcommerce.Domain.Abstract;
using SportsEcommerce.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsEcommerce.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository productRepo;

        public int PageSize = 2;

        public ProductController(IProductRepository productRepo)
        {
            this.productRepo = productRepo;
        }

        // GET: Products
        public ViewResult List(int page = 1)
        {
            ProductListViewModel model = new ProductListViewModel
            {
                Products = productRepo.Products
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = productRepo.Products.Count()
                }
            };


            return View(model);
        }
    }
}