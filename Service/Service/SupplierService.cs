
using AutoMapper;
using Core.Entities;

public class SupplierService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public SupplierService(
        IUnitOfWork unitOfWork,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    #region Supplier
    public async Task<IEnumerable<SupplierDTO>> GetSupplierAsync()
    {
        var projects = await _unitOfWork.Supplier.GetAll();

        return _mapper.Map<IEnumerable<SupplierDTO>>(projects);
    }

    public async Task<bool> InsertAsync(SupplierDTO projectDTO)
    {
        var project = _mapper.Map<Supplier>(projectDTO);
        return await _unitOfWork.Supplier.Add(project);
    }
    #endregion

    public async Task<int> CompletedAsync()
    {
        return await _unitOfWork.CompletedAsync();
    }
}
