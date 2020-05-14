using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Infrastructure;
using Microsoft.Extensions.Configuration;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers.API
{
    [Route("api/[controller]")]
    public class SalesOrderController : Controller
    {
        private readonly string _connectionString;
        public SalesOrderController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BrainWareConnectionString");
         }
        //public SalesOrderController(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}
        // GET: api/<controller>
        [HttpGet]
        public IActionResult GetOrders()
        {
            var data = new SalesOrderService(_connectionString);

            return View(data.GetAllSalesOrder());
        }

        [HttpPost]
        public IActionResult GetOrdersByPage(int pageSize = 10, int pageNumber = 1)
        {
            var data = new SalesOrderService(_connectionString);
                        
            return View(data.GetAllSalesOrder(pageSize,pageNumber));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult GetOrderById(string id)
        {
            var data = new SalesOrderService(_connectionString);

            return View(data.GetSalesOrderByID(id));
        }

        // POST api/<controller>
        [HttpPost]
        public void Create([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
