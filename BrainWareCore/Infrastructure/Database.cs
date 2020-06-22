using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Extensions.Configuration;

namespace Web.Infrastructure
{
    using System.Data;
    using System.Data.Common;
   
    using System.Data.SqlClient;
    using Web.Models;

    public class Database
    {
        private readonly SqlConnection _connection;

        public Database(string connectionString)
        {
            // var connectionString = "Data Source=LOCALHOST;Initial Catalog=BrainWare;Integrated Security=SSPI";
            //var mdf = @"C:\Brainshark\interview\BrainWare\Web\App_Data\BrainWare.mdf";
            //var connectionString = $"Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=BrainWAre;Integrated Security=SSPI;AttachDBFilename={mdf}";
            //var connectionString = ConfigurationManager.ConnectionStrings["RetailConnectionString"].ConnectionString;
            _connection = new SqlConnection(connectionString);

            _connection.Open();
        }
        public DbDataReader ExecuteReader(Guid id)
        {
            var query =
               "SELECT c.name, o.description, o.SalesOrderid FROM company c INNER JOIN [SalesOrder] o on c.companyid=o.companyid";

            var sqlQueryCommand = new SqlCommand(query, _connection);
            sqlQueryCommand.Parameters.Add(new SqlParameter("Id", id));
            return sqlQueryCommand.ExecuteReader();
        }
        public DbDataReader ExecuteReader(string query)
        {          
            var sqlQuery = new SqlCommand(query, _connection);

            return sqlQuery.ExecuteReader();
        }
        public List<SalesOrder> GetAllSalesOrders(int pageSize=1000, int pageNumber =1)
        {
            // Get the SalesOrders
                            var query =
                "SELECT c.name, o.description, o.SalesOrderid ,op.price,  op.productid, op.quantity, p.name, p.price "+
                "FROM company c "+
                "INNER JOIN[SalesOrder] o on c.companyid = o.companyid "+
                "INNER JOIN SalesOrderDetail op on op.SalesOrderId = o.SalesOrderId "+
                "INNER JOIN product p on op.productid = p.productid "+
                "Order By o.SalesOrderId " +
                "OFFSET (({0}) * {1}) ROWS " +
                "FETCH NEXT {2} ROWS ONLY ";
            var paramSupplied = String.Format(query, (pageNumber - 1), pageSize, pageSize);
            var sqlQuery = new SqlCommand(paramSupplied, _connection);
            
            var reader1 = sqlQuery.ExecuteReader();

            var values = new List<SalesOrder>();

            while (reader1.Read())
            {
                var record1 = (IDataRecord)reader1;
                var companyName = record1.GetString(0);
                var companyExist = new List<SalesOrder>();
                if (values.Count != 0)
                { 
                    companyExist = values.Where(t => t.CompanyName == companyName.Trim()).ToList(); 
                }
                if (companyExist.Count !=0 && companyExist[0].SalesOrderDetails != null) 
                {
                    companyExist[0].SalesOrderDetails.Add(new SalesOrderDetail()
                    {
                        SalesOrderId = record1.GetGuid(2),
                        ProductId = record1.GetGuid(4),
                        Price = record1.GetDecimal(3),
                        Quantity = record1.GetInt32(5),
                        Product = new Product()
                        {
                            Name = record1.GetString(6),
                            Price = record1.GetDecimal(7)
                        }
                    });
                }
                else
                {
                    var salesOrder=new SalesOrder()
                    {
                        CompanyName = record1.GetString(0),
                        Description = record1.GetString(1),
                        SalesOrderId = record1.GetGuid(2),
                        SalesOrderDetails = new List<SalesOrderDetail>()
                    };
                    salesOrder.SalesOrderDetails.Add(new SalesOrderDetail()
                     {
                         SalesOrderId = record1.GetGuid(2),
                         ProductId = record1.GetGuid(4),
                         Price = record1.GetDecimal(3),
                         Quantity = record1.GetInt32(5),
                         Product = new Product()
                         {
                             Name = record1.GetString(6),
                             Price = record1.GetDecimal(7)
                         }
                     });
                    values.Add(salesOrder);
                }
            }

            reader1.Close();
            return values;
        }
        public List<SalesOrderDetail> GetProductsForSalesOrders()
        {
            //Get the SalesOrder products
            var query = 
               "SELECT op.price, op.SalesOrderid, op.productid, op.quantity, p.name, p.price " +
               "FROM SalesOrderDetail op " +
               "INNER JOIN product p on op.productid=p.productid";
           
            var sqlQueryCommand = new SqlCommand(query, _connection);
           // sqlQueryCommand.Parameters.Add(new SqlParameter("Id", id));
  
            var reader2 = sqlQueryCommand.ExecuteReader();

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
            return values2;
        }
        public int ExecuteNonQuery(string query)
        {
            var sqlQuery = new SqlCommand(query, _connection);

            return sqlQuery.ExecuteNonQuery();
        }

    }
}