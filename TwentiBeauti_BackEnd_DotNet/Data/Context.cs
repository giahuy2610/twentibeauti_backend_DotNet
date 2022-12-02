using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using TwentiBeauti_BackEnd_DotNet.Models;

namespace TwentiBeauti_BackEnd_DotNet.Data
{
    public class Context : DbContext

    {
        internal static object dbContext;

        public string ConnectionString { get; set; }//biết thành viên 

        public Context(string connectionString) //phuong thuc khoi tao
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection() //lấy connection 
        {
            return new MySqlConnection(ConnectionString);
        }
        public Context(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Coupon> Coupon { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetail { get; set; }
        public DbSet<Invoice> Invoice { get; set; }


    }

}