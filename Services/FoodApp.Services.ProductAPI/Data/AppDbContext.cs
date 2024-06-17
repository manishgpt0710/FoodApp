using FoodApp.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodApp.Services.ProductAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(new Product 
            { ProductId = 1, Name = "Samosa", Price = 10, Description = "Tasty Samosa", ImageUrl = "~/images/Samosa-and-Chatni.jpg", CategoryName = "Snacks" });
            modelBuilder.Entity<Product>().HasData(new Product
            { ProductId = 2, Name = "Paneer Tikka", Price = 200, Description = "Paneer Tikka with capsicum", ImageUrl = "~/images/paneer-tikka.jpg", CategoryName = "Snacks" });
        }
    }
}
