using Microsoft.AspNetCore.Mvc;

public class ProductController : Controller
{
    private readonly ProductService _service;
    public ProductController(ProductService productService)
    {
        _service = productService;
    }


    // GET: AccountController
    public async Task<IEnumerable<ProductDTO>> Index()
    {
        var products = await _service.GetProductAsync();
        return products;
    }
}