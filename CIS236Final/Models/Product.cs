﻿// Product.cs
using System.ComponentModel.DataAnnotations;

namespace CIS236Final.Models;
public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Price is required")]
    public decimal Price { get; set; }
}
