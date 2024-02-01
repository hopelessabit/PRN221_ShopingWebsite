
using AutoMapper;
using Core.Entities;

public class OrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public OrderService(
        IUnitOfWork unitOfWork,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    #region Order
    public async Task<IEnumerable<OrderDTO>> GetOrderAsync()
    {
        var projects = await _unitOfWork.Order.GetAll();

        return _mapper.Map<IEnumerable<OrderDTO>>(projects);
    }

    public async Task<bool> InsertAsync(OrderDTO projectDTO)
    {
        var project = _mapper.Map<Order>(projectDTO);
        return await _unitOfWork.Order.Add(project);
    }

    #endregion

    public async Task<int> CompletedAsync()
    {
        return await _unitOfWork.CompletedAsync();
    }
}
