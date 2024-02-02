using Core.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
 
namespace Shop.Pages.Productss
{
    public class Create : PageModel
    {
        private readonly ProductService _productService; // Assume ProductService for product CRUD operations

        public Create(ProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Product Product { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Only allow staff members to create products (assuming type 0 is staff)
            if (User.IsInRole("Staff"))
            {
                _productService.CreateProduct(Product);
                return RedirectToPage("/Products/Index");
            }

            return Forbid(); // User doesn't have the required permission
        }
    }
}