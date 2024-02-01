
using AutoMapper;
using Core.Entities;

public class CategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CategoryService(
        IUnitOfWork unitOfWork,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    #region Category
    public async Task<IEnumerable<CategoryDTO>> GetCategoryAsync()
    {
        var projects = await _unitOfWork.Category.GetAll();

        return _mapper.Map<IEnumerable<CategoryDTO>>(projects);
    }

    public async Task<bool> InsertAsync(CategoryDTO projectDTO)
    {
        var project = _mapper.Map<Category>(projectDTO);
        return await _unitOfWork.Category.Add(project);
    }
    #endregion

    public async Task<int> CompletedAsync()
    {
        return await _unitOfWork.CompletedAsync();
    }
}
