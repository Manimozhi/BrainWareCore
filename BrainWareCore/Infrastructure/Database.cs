using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Extensions.Configuration;

namespace Web.Infrastructure
{
    using System.Data.Common;
   
    using System.Data.SqlClient;

    public class Database
    {
        private readonly SqlConnection _connection;

        public Database(string connectionString)
        {
            // var connectionString = "Data Source=LOCALHOST;Initial Catalog=BrainWare;Integrated Security=SSPI";
            //var mdf = @"C:\Brainshark\interview\BrainWare\Web\App_Data\BrainWare.mdf";
            //var connectionString = $"Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=BrainWAre;Integrated Security=SSPI;AttachDBFilename={mdf}";
            //var connectionString = ConfigurationManager.ConnectionStrings["BrainWareConnectionString"].ConnectionString;
            _connection = new SqlConnection(connectionString);

            _connection.Open();
        }

        public DbDataReader ExecuteReader(string query)
        {
           

            var sqlQuery = new SqlCommand(query, _connection);

            return sqlQuery.ExecuteReader();
        }

        public int ExecuteNonQuery(string query)
        {
            var sqlQuery = new SqlCommand(query, _connection);

            return sqlQuery.ExecuteNonQuery();
        }

    }
}