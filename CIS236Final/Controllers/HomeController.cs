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
            
            if (ModelState.IsValid)
            {
                // Add the new product to the database
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // Action method to display the form for editing an existing product
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

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
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // Action method to display the confirmation page for deleting a product
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST action method to handle the deletion of a product
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            // Remove the product from the database
            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
