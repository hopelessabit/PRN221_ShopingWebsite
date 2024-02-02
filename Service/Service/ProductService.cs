
using AutoMapper;
using Core.Entities;
using Core.Repositories;

public class ProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProductService(
        IUnitOfWork unitOfWork,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    #region Product
    public async Task<IEnumerable<ProductDTO>> GetProductAsync()
    {
        var projects = await _unitOfWork.Product.GetAll();

        return _mapper.Map<IEnumerable<ProductDTO>>(projects);
    }

    public async Task<bool> InsertProductAsync(ProductDTO projectDTO)
    {
        var project = _mapper.Map<Product>(projectDTO);
        return await _unitOfWork.Product.Add(project);
    }

    #endregion

    public async Task<int> CompletedAsync()
    {
        return await _unitOfWork.CompletedAsync();
    }
    private readonly ShopContext _dbContext; // Replace with your actual DbContext

    public ProductService(ShopContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateProduct(Product product)
    {
        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();
    }
    public IEnumerable<Product> GetAllProducts()
    {
        return _dbContext.Products.ToList();
    }

    public Product GetProductById(int productId)
    {
        return _dbContext.Products.Find(productId);
    }

    public void UpdateProduct(Product updatedProduct)
    {
        var existingProduct = _dbContext.Products.Find(updatedProduct.ProductId);

        if (existingProduct != null)
        {
            _dbContext.Entry(existingProduct).CurrentValues.SetValues(updatedProduct);
            _dbContext.SaveChanges();
        }
        
    }

    public void DeleteProduct(int productId)
    {
        var productToRemove = _dbContext.Products.Find(productId);

        if (productToRemove != null)
        {
            _dbContext.Products.Remove(productToRemove);
            _dbContext.SaveChanges();
        }
        
    }
}
