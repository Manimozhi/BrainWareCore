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

            // Get the SalesOrders
            var sql1 =
                "SELECT c.name, o.description, o.SalesOrderid FROM company c INNER JOIN [SalesOrder] o on c.companyid=o.companyid";

            var reader1 = database.ExecuteReader(sql1);

            var values = new List<SalesOrder>();
            
            while (reader1.Read())
            {
                var record1 = (IDataRecord) reader1;

                values.Add(new SalesOrder()
                {
                    CompanyName = record1.GetString(0),
                    Description = record1.GetString(1),
                    SalesOrderId = record1.GetGuid(2),
                    SalesOrderDetails = new List<SalesOrderDetail>()
                });

            }

            reader1.Close();

            //Get the SalesOrder products
            var sql2 =
                "SELECT op.price, op.SalesOrderid, op.productid, op.quantity, p.name, p.price FROM SalesOrderDetail op INNER JOIN product p on op.productid=p.productid";

            var reader2 = database.ExecuteReader(sql2);

            var values2 = new List<SalesOrderDetail>();

            while (reader2.Read())
            {
                var record2 = (IDataRecord)reader2;

                values2.Add(new SalesOrderDetail()
                {
                    SalesOrderId = record2.GetGuid(1),
                    ProductId = record2.GetGuid(2),
                    Price = record2.GetDecimal(0),
                    Quantity = record2.GetInt32(3),
                    Product = new Product()
                    {
                        Name = record2.GetString(4),
                        Price = record2.GetDecimal(5)
                    }
                });
             }

            reader2.Close();

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
            var database = _database;

        // Get the SalesOrders
            var sql1 =
            "SELECT c.name, o.description, o.SalesOrderid FROM company c INNER JOIN [SalesOrder] o on c.companyid=o.companyid and o.SalesOrderid='{0}'";
            var queryWithParam =  String.Format(sql1,Id);  

        var reader1 = database.ExecuteReader(sql1);

        var values = new List<SalesOrder>();

        while (reader1.Read())
        {
            var record1 = (IDataRecord)reader1;

            values.Add(new SalesOrder()
            {
                CompanyName = record1.GetString(0),
                Description = record1.GetString(1),
                SalesOrderId = record1.GetGuid(2),
                SalesOrderDetails = new List<SalesOrderDetail>()
            });

        }

        reader1.Close();

        //Get the SalesOrder products
        
        var sql2 =
            "SELECT op.price, op.SalesOrderid, op.productid, op.quantity, p.name, p.price FROM SalesOrderDetail op INNER JOIN product p on op.productid=p.productid";

        var reader2 = database.ExecuteReader(sql2);

        var values2 = new List<SalesOrderDetail>();

        while (reader2.Read())
        {
            var record2 = (IDataRecord)reader2;

            values2.Add(new SalesOrderDetail()
            {
                SalesOrderId = record2.GetGuid(1),
                ProductId = record2.GetGuid(2),
                Price = record2.GetDecimal(0),
                Quantity = record2.GetInt32(3),
                Product = new Product()
                {
                    Name = record2.GetString(4),
                    Price = record2.GetDecimal(5)
                }
            });
        }

        reader2.Close();

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
    }
}
