using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Extensions.Configuration;

namespace Web.Infrastructure
{
    using System.Data;
    using Models;

    public class SalesOrderService
    {
        Database _database ;
        public SalesOrderService(IConfiguration configuration)
        {
            _database = new Database(configuration.GetConnectionString("DBConnection"));
        }
        public SalesOrderService(string ConnectionString)
        {
            _database = new Database(ConnectionString);
        }
        public List<SalesOrder> GetAllSalesOrder()
        {
            var database = _database;
            var values = database.GetAllSalesOrders();
            var values2 = database.GetProductsForSalesOrders();

            foreach (var SalesOrder in values)
            {
                foreach (var SalesOrderDetail in values2)
                {
                    if (SalesOrderDetail.SalesOrderId != SalesOrder.SalesOrderId)
                        continue;

                    SalesOrder.SalesOrderDetails.Add(SalesOrderDetail);
                    SalesOrder.SalesOrderTotal = SalesOrder.SalesOrderTotal + (SalesOrderDetail.Price * SalesOrderDetail.Quantity);
                }
            }

            return values;
        }
        public List<SalesOrder> GetAllSalesOrder(int pageSize, int pageNumber)
        {
            var database = _database;
            var values = database.GetAllSalesOrders();
            var values2 = database.GetProductsForSalesOrders();

            foreach (var SalesOrder in values)
            {
                foreach (var SalesOrderDetail in values2)
                {
                    if (SalesOrderDetail.SalesOrderId != SalesOrder.SalesOrderId)
                        continue;

                    SalesOrder.SalesOrderDetails.Add(SalesOrderDetail);
                    SalesOrder.SalesOrderTotal = SalesOrder.SalesOrderTotal + (SalesOrderDetail.Price * SalesOrderDetail.Quantity);
                }
            }

            return values;
        }
        public List<SalesOrder> GetSalesOrderByID(String Id)
        {            
            var values = new List<SalesOrder>();
            return values;
        }
    }
}
