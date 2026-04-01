using WebApplication1.Data;
using WebApplication1.DTOs;

namespace WebApplication1.services
{
    public interface ICategoryService
    {
        Task<CategoryReadDto> CreateCategoryAsync(CategoryCreateDto dto);
        Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync();
        Task<Category?> FindByNameAsync(string name);
    }
}
