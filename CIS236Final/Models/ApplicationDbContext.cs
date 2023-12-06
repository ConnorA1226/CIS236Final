// ApplicationDbContext.cs
using CIS236Final.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CIS236Final.Models
{
    // Represents the database context for the application
    public class ApplicationDbContext : DbContext
    {
        // Constructor to initialize the context
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        // Override the default behavior when the model is being created
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the 'Price' property of the 'Product' entity to use decimal(18, 2) in the database
            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18, 2)");

            // Seed the 'Products' table with initial data
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Cookies", Price = 19.99m },
                new Product { Id = 2, Name = "Milk", Price = 29.99m }
            );
        }
    }
}
