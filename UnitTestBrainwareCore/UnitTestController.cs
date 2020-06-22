using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Web.Controllers;
using Web.Controllers.API;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BrainWare2019;
using Web.Infrastructure;
using System;

namespace UnitTestBrainware2019
{
    [TestClass]
    public class UnitTestController
    {
        private  string ConnectionString;
        // requires using Microsoft.Extensions.Configuration;
        private readonly IConfiguration Configuration;
        private readonly ILogger<HomeController> _logger;
        public UnitTestController()
        {
           // Configuration = configuration;
        }
        // [SetUp]
        public void SetUp()
        {
           // ConnectionString = Configuration["RetailConnectionString"];
            
            ConnectionString = "Server=tcp:cephalin-core.database.windows.net,1433;Database=retailDB;User ID=mdoraisamy;Password=Muthaluser9874;Encrypt=true;Connection Timeout=30;";           
        }
        //[TearDown]
        public void TearDown()
        {
        }
        
       
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
            SalesOrderController orderController = new SalesOrderController(Configuration);

            var result = orderController.GetOrders();
            Assert.IsNotNull(result);

            Assert.IsNotNull(result);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void GetAllSalesOrdersTest()
        {
            SetUp();
            var dataBase = new Database(ConnectionString);
            var result = dataBase.GetAllSalesOrders();
             
            Assert.IsNotNull(result);

            Assert.IsNotNull(result);
            Assert.IsTrue(true);
        }
        [TestMethod]
        public void GetAllSalesOrdersPagedTest()
        {
            SetUp();
            var dataBase = new Database(ConnectionString);
            var result = dataBase.GetAllSalesOrders(10,1);

            Assert.IsNotNull(result);

            Assert.AreEqual(5,result[0].SalesOrderDetails.Count);
            Assert.AreEqual("Home Insurance",result[0].CompanyName.Trim());
            Assert.IsTrue(true);
        }
    }
}
