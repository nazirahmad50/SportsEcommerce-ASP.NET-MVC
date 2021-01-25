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

        public int PageSize  { get; set; } = 2;

        public ProductController(IProductRepository productRepo)
        {
            this.productRepo = productRepo;
        }

        // GET: Products
        public ViewResult List(string category, int page = 1)
        {
            ProductListViewModel model = new ProductListViewModel
            {
                Products = productRepo.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null 
                                ? productRepo.Products.Count() 
                                :productRepo.Products.Where(c => c.Category == category).Count(),
                    
                },
                 CurrentCategory = category
            };


            return View(model);
        }
    }
}