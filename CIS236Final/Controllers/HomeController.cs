using CIS236Final.Models;
using Microsoft.AspNetCore.Mvc;

namespace CIS236Final.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Action method to display a list of products based on search and filter criteria
        public IActionResult Index(string searchString, decimal? filterPrice)
        {
            // Get all products from the database
            var products = _context.Products.AsQueryable();

            // Apply search filter if searchString is provided
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString));
            }

            // Apply price filter if filterPrice is provided
            if (filterPrice.HasValue)
            {
                products = products.Where(p => p.Price == filterPrice.Value);
            }

            // Pass the list of filtered products to the view
            return View(products.ToList());
        }

        // Action method to display the form for creating a new product
        public IActionResult Create()
        {
            return View();
        }

        // POST action method to handle the creation of a new product
        [HttpPost]
        public IActionResult Create(Product product)
        {
            // Check if the model is valid before saving
            if (ModelState.IsValid)
            {
                // Add the new product to the database
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Redirect to the product list
            }

            // If the model is not valid, return to the create view with validation errors
            return View(product);
        }

        // Action method to display the form for editing an existing product
        public IActionResult Edit(int id)
        {
            // Find the product with the specified id
            var product = _context.Products.Find(id);

            // If the product is not found, return a not found result
            if (product == null)
            {
                return NotFound();
            }

            // Pass the product to the view for editing
            return View(product);
        }

        // POST action method to handle the editing of an existing product
        [HttpPost]
        public IActionResult Edit(int id, Product product)
        {
            // Check if the provided id matches the id of the edited product
            if (id != product.Id)
            {
                return NotFound();
            }

            // Check if the model is valid before updating
            if (ModelState.IsValid)
            {
                // Update the product in the database
                _context.Update(product);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Redirect to the product list
            }

            // If the model is not valid, return to the edit view with validation errors
            return View(product);
        }

        // Action method to display the confirmation page for deleting a product
        public IActionResult Delete(int id)
        {
            // Find the product with the specified id
            var product = _context.Products.Find(id);

            // If the product is not found, return a not found result
            if (product == null)
            {
                return NotFound();
            }

            // Pass the product to the view for confirmation
            return View(product);
        }

        // POST action method to handle the deletion of a product
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Find the product with the specified id
            var product = _context.Products.Find(id);

            // If the product is not found, return a not found result
            if (product == null)
            {
                return NotFound();
            }

            // Remove the product from the database
            _context.Products.Remove(product);
            _context.SaveChanges();

            // Redirect to the product list after deletion
            return RedirectToAction("Index");
        }
    }
}
