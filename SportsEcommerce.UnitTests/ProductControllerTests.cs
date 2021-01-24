using Moq;
using NUnit.Framework;
using SportsEcommerce.Domain.Abstract;
using SportsEcommerce.Domain.Entities;
using SportsEcommerce.WebUI.Controllers;
using SportsEcommerce.WebUI.HtmlHelpers;
using SportsEcommerce.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsEcommerce.UnitTests
{
    [TestFixture]
    public class ProductControllerTests
    {
        private Mock<IProductRepository> mock;
        private ProductController controller;

        [SetUp]
        public void Init()
        {
            mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ProductID = 1, Name = "P1"},
                new Product{ProductID = 2, Name = "P2"},
                new Product{ProductID = 3, Name = "P3"},
                new Product{ProductID = 4, Name = "P4"},
                new Product{ProductID = 5, Name = "P5"},
            });

            controller = new ProductController(mock.Object)
            {
                PageSize = 3
            };

        }

        [Test]
        public void Can_Paginate_Products()
        {
            // act
            ProductListViewModel result = (ProductListViewModel)controller.List(2).Model;

            // assrt
            Product[] productArray = result.Products.ToArray();
            Assert.IsTrue(productArray.Length == 2);
            Assert.AreEqual(productArray[0].Name, "P4");
            Assert.AreEqual(productArray[1].Name, "P5");

        }

        [Test]
        public void Can_Paginate_Page_Links()
        {
            // define an html helper - this needs to be done in order to apply the extension method
            HtmlHelper helper = null;

            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 8
            };

            // act
            MvcHtmlString result = helper.PageLinks(pagingInfo, i => "Page" + i);

            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                          + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                          + @"<a class=""btn btn-default"" href=""Page3"">3</a>", result.ToString());

        }

        [Test]
        public void Can_Send_Pagination_View_Model()
        {
            // act
            ProductListViewModel result = (ProductListViewModel)controller.List(2).Model;

            // assrt
            PagingInfo pagingInfo = result.PagingInfo;
            Assert.AreEqual(pagingInfo.CurrentPage, 2);
            Assert.AreEqual(pagingInfo.ItemsPerPage, 3);
            Assert.AreEqual(pagingInfo.TotalItems, 5);
            Assert.AreEqual(pagingInfo.TotalPages, 2);
        }

    }
}
