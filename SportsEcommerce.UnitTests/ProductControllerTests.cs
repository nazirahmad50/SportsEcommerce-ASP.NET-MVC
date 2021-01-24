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
                new Product{ProductID = 1, Name = "P1", Category = "Cat2"},
                new Product{ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product{ProductID = 3, Name = "P3", Category = "Cat3"},
                new Product{ProductID = 4, Name = "P4", Category = "Cat4"},
                new Product{ProductID = 5, Name = "P5", Category = "Cat5"},
            });

            controller = new ProductController(mock.Object)
            {
                PageSize = 2
            };

        }

        [Test]
        public void Can_Paginate_Products()
        {
            // act
            ProductListViewModel result = (ProductListViewModel)controller.List(null, 2).Model;

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
            ProductListViewModel result = (ProductListViewModel)controller.List(null, 2).Model;

            PagingInfo pagingInfo = result.PagingInfo;
            Assert.AreEqual(pagingInfo.CurrentPage, 2);
            Assert.AreEqual(pagingInfo.ItemsPerPage, 3);
            Assert.AreEqual(pagingInfo.TotalItems, 5);
            Assert.AreEqual(pagingInfo.TotalPages, 2);
        }

        [Test]
        public void Can_Filter_Products_By_Category()
        {
            // act
            Product[] result = ((ProductListViewModel)controller.List("Cat2", 1).Model).Products.ToArray();


            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P1" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "P2" && result[1].Category == "Cat2");

        }

        [Test]
        public void Generate_Category_Specific_Page_Count()
        {

            int res1 = ((ProductListViewModel)controller.List("Cat2").Model).PagingInfo.TotalItems;
            int res2 = ((ProductListViewModel)controller.List("Cat3").Model).PagingInfo.TotalItems;
            int resAll = ((ProductListViewModel)controller.List(null).Model).PagingInfo.TotalItems;


            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 1);
            Assert.AreEqual(resAll, 5);

        }

    }
}
