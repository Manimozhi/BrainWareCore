using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Web.Controllers;
using Web.Controllers.API;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BrainWare2019;

namespace UnitTestBrainware2019
{
    [TestClass]
    public class UnitTestController
    {
        private  string _connectionString;
       // [SetUp]
        public void SetUp()
        {

            _connectionString = "Server=.;Database=BrainWare;Trusted_Connection=True;";           
            
        }
        //[TearDown]
        public void TearDown()
        {
        }
        
        private readonly ILogger<HomeController> _logger;
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(_logger);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewData["Title"]);
        }

        [TestMethod]
        public void Test()
        {
            SetUp();
            SalesOrderController orderController = new SalesOrderController(_connectionString);

            var result = orderController.Get();
            Assert.IsNotNull(result);



            string id = "2D6EB4F1-2C4C-463E-A810-0063EE466B4A";
           // result = orderController.GetOrdersById(id) ;

            Assert.IsNotNull(result);
            Assert.IsTrue(true);
        }
    }
}
