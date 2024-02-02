
using AutoMapper;
using Core.Entities;

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

    public async Task<bool> UpdateProductAsync(ProductDTO projectDTO)
    {
        var project = _mapper.Map<Product>(projectDTO);
        return await _unitOfWork.Product.Upsert(project);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        return await _unitOfWork.Product.Remove(id);
    }
    #endregion

    public async Task<int> CompletedAsync()
    {
        return await _unitOfWork.CompletedAsync();
    }
}
