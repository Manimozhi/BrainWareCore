using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    using System.Security.AccessControl;

    public class SalesOrder
    {
        public Guid SalesOrderId { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }

        public decimal SalesOrderTotal { get; set; }

        public List<SalesOrderDetail> SalesOrderDetails { get; set; }

    }


    public class SalesOrderDetail
    {
        public Guid SalesOrderId { get; set; }

        public Guid ProductId { get; set; }

        public Product Product { get; set; }
    
        public int Quantity { get; set; }

        public decimal Price { get; set; }

    }

    public class Product
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}