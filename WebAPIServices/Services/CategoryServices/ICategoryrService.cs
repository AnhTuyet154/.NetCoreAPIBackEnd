using WebAPIServices.Dto.Category;

namespace WebAPIServices.Services.CategoryServices
{
    public interface ICategoryrService
    {
        Task<List<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> GetSingleCategoryAsync(int id);
        Task<CategoryDto> AddCategoryAsync(CreateCategoryDto categoryDto);
        Task<CategoryDto> UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto);
        Task<List<CategoryDto>> DeleteCategoryAsync(int id);
    }
}
