
using AutoMapper;
using Core.Entities;

public class OrderDetailService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public OrderDetailService(
        IUnitOfWork unitOfWork,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    #region OrderDetail
    public async Task<IEnumerable<OrderDetailDTO>> GetOrderDetailAsync()
    {
        var projects = await _unitOfWork.OrderDetail.GetAll();

        return _mapper.Map<IEnumerable<OrderDetailDTO>>(projects);
    }

    public async Task<bool> InsertDetailAsync(OrderDetailDTO projectDTO)
    {
        var project = _mapper.Map<OrderDetail>(projectDTO);
        return await _unitOfWork.OrderDetail.Add(project);
    }
    #endregion

    public async Task<int> CompletedAsync()
    {
        return await _unitOfWork.CompletedAsync();
    }
}
