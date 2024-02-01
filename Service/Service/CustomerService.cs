
using AutoMapper;
using Core.Entities;

public class CustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CustomerService(
        IUnitOfWork unitOfWork,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    #region Customer
    public async Task<IEnumerable<CustomerDTO>> GetCustomerAsync()
    {
        var projects = await _unitOfWork.Customer.GetAll();

        return _mapper.Map<IEnumerable<CustomerDTO>>(projects);
    }

    public async Task<bool> InsertAsync(CustomerDTO projectDTO)
    {
        var project = _mapper.Map<Customer>(projectDTO);
        return await _unitOfWork.Customer.Add(project);
    }
    #endregion

    public async Task<int> CompletedAsync()
    {
        return await _unitOfWork.CompletedAsync();
    }
}
