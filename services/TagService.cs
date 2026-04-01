using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;

namespace WebApplication1.services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDB _dbContext;

        public CategoryService(ApplicationDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CategoryReadDto> CreateCategoryAsync(CategoryCreateDto dto)
        {
            var category = new Category
            {
                Name = dto.Name.Trim()
            };

            await _dbContext.categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return new CategoryReadDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<Category?> FindByNameAsync(string name)
        {
            return await _dbContext.categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Name.ToLower() == name.Trim().ToLower());
        }

        public async Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync()
        {
            return await _dbContext.categories
                .AsNoTracking()
                .Select(c => new CategoryReadDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }
    }
}
