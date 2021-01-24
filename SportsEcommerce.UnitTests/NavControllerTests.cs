using Moq;
using NUnit.Framework;
using SportsEcommerce.Domain.Abstract;
using SportsEcommerce.Domain.Entities;
using SportsEcommerce.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsEcommerce.UnitTests
{
    public class NavControllerTests
    {
        private Mock<IProductRepository> mock;
        private NavController controller;

        [SetUp]
        public void Init()
        {
            mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product{ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product{ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product{ProductID = 3, Name = "P3", Category = "Cat3"},
            });

            controller = new NavController(mock.Object);
          
        }

        [Test]
        public void Can_Create_Categories()
        {
            string[] results = ((IEnumerable<string>)controller.Menu().Model).ToArray();

            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0], "Cat1");
            Assert.AreEqual(results[1], "Cat2");
            Assert.AreEqual(results[2], "Cat3");
        }

        [Test]
        public void Indicates_Selected_Category()
        {
            string categorySelected = "Cat1";
            
            string result = controller.Menu(categorySelected).ViewBag.SelectedCategory;

            Assert.AreEqual(result, "Cat1");
        }
    }
}
