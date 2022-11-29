using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using TwentiBeauti_BackEnd_DotNet.Models;

namespace TwentiBeauti_BackEnd_DotNet.Data
{
    public class Context : DbContext

    {
        public Context(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Coupon> Coupon { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetail { get; set; }
    }
}
