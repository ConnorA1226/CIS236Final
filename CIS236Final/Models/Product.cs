// Product.cs
using System.ComponentModel.DataAnnotations;

namespace CIS236Final.Models
{
    // Product class representing a product entity in the application
    public class Product
    {
        // Unique identifier for the product
        public int Id { get; set; }

        // Name of the product, with validation for required field
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        // Price of the product, with validation for required field
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
    }
}
