using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Reflection.Metadata;
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

        

        public DbSet<Address> Address { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Collection> Collection { get; set; }
        public DbSet<Coupon> Coupon { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<ImageSlider> ImageSlider { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetail { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<PromotionRegister> PromotionRegister { get; set; }
        public DbSet<RetailPrice> RetailPrice { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<ReviewImage> ReviewImage { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<TypeProduct> TypeProduct { get; set; }

        public DbSet<CollectionProduct> CollectionProduct { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoiceDetail>().HasKey(vf => new { vf.IDProduct, vf.IDInvoice });
            modelBuilder.Entity<CollectionProduct>().HasKey(vf => new { vf.IDProduct, vf.IDCollection });
            modelBuilder.Entity<Cart>().HasKey(vf => new { vf.IDProduct, vf.IDCus });
        }





    }

}