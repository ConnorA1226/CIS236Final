// ApplicationDbContext.cs
using CIS236Final.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CIS236Final.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Cookies", Price = 19.99m },
                new Product { Id = 2, Name = "Milk", Price = 29.99m }
            );
        }
    }
}
