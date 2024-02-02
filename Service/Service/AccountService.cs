
using AutoMapper;
using Core.Entities;
public class AccountService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public AccountService(
        IUnitOfWork unitOfWork,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    #region Account
    public async Task<IEnumerable<AccountDTO>> GetAccountAsync()
    {
        var projects = await _unitOfWork.Account.GetAll();

        return _mapper.Map<IEnumerable<AccountDTO>>(projects);
    }

    public async Task<bool> InsertAsync(AccountDTO projectDTO)
    {
        var project = _mapper.Map<Account>(projectDTO);
        return await _unitOfWork.Account.Add(project);
    }
    #endregion

    public async Task<int> CompletedAsync()
    {
        return await _unitOfWork.CompletedAsync();
    }
}
