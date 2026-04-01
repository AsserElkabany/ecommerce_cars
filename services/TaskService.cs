using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;

namespace WebApplication1.services
{
    public class CarService : ICarService
    {
        private readonly ApplicationDB _dbContext;

        public CarService(ApplicationDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CarReadDto> CreateCarAsync(CarCreateDto dto)
        {
            var car = new Car
            {
                Make = dto.Make,
                Model = dto.Model,
                Description = dto.Description,
                Price = dto.Price,
                Year = dto.Year,
                IsAvailable = true,
                SellerUserId = dto.SellerUserId
            };

            if (dto.CategoryIds.Any())
            {
                var categories = await _dbContext.categories.Where(c => dto.CategoryIds.Contains(c.Id)).ToListAsync();
                foreach (var category in categories)
                {
                    car.CarCategories.Add(new CarCategory { CategoryId = category.Id, Category = category, Car = car });
                }
            }

            await _dbContext.cars.AddAsync(car);
            await _dbContext.SaveChangesAsync();

            return await GetCarByIdAsync(car.Id) ?? throw new InvalidOperationException("Car was created but could not be loaded.");
        }

        public async Task<bool> DeleteCarAsync(int id)
        {
            var existing = await _dbContext.cars.FindAsync(id);
            if (existing == null)
            {
                return false;
            }

            _dbContext.cars.Remove(existing);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CarReadDto>> GetAllCarsAsync()
        {
            return await _dbContext.cars
                .AsNoTracking()
                .Include(c => c.Seller)
                .Include(c => c.CarCategories)
                    .ThenInclude(cc => cc.Category)
                .Select(c => new CarReadDto
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Description = c.Description,
                    Price = c.Price,
                    Year = c.Year,
                    IsAvailable = c.IsAvailable,
                    SellerUserId = c.SellerUserId,
                    SellerName = c.Seller != null ? c.Seller.Name : string.Empty,
                    Categories = c.CarCategories.Select(cc => cc.Category.Name).ToList()
                })
                .ToListAsync();
        }

        public async Task<CarReadDto?> GetCarByIdAsync(int id)
        {
            var car = await _dbContext.cars
                .AsNoTracking()
                .Include(c => c.Seller)
                .Include(c => c.CarCategories)
                    .ThenInclude(cc => cc.Category)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                return null;
            }

            return new CarReadDto
            {
                Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                Description = car.Description,
                Price = car.Price,
                Year = car.Year,
                IsAvailable = car.IsAvailable,
                SellerUserId = car.SellerUserId,
                SellerName = car.Seller?.Name ?? string.Empty,
                Categories = car.CarCategories.Select(cc => cc.Category.Name).ToList()
            };
        }

        public async Task<bool> SetCarAvailabilityAsync(int id, bool isAvailable)
        {
            var existing = await _dbContext.cars.FindAsync(id);
            if (existing == null)
            {
                return false;
            }

            existing.IsAvailable = isAvailable;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AssignCarAsync(int id, int sellerUserId)
        {
            var existing = await _dbContext.cars.FindAsync(id);
            if (existing == null)
            {
                return false;
            }

            var user = await _dbContext.users.FindAsync(sellerUserId);
            if (user == null)
            {
                return false;
            }

            existing.SellerUserId = sellerUserId;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCarAsync(int id, CarUpdateDto dto)
        {
            var existing = await _dbContext.cars
                .Include(c => c.CarCategories)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existing == null)
            {
                return false;
            }

            existing.Make = dto.Make;
            existing.Model = dto.Model;
            existing.Description = dto.Description;
            existing.Price = dto.Price;
            existing.Year = dto.Year;
            existing.IsAvailable = dto.IsAvailable;
            existing.SellerUserId = dto.SellerUserId;

            existing.CarCategories.Clear();
            if (dto.CategoryIds.Any())
            {
                var categories = await _dbContext.categories.Where(c => dto.CategoryIds.Contains(c.Id)).ToListAsync();
                foreach (var category in categories)
                {
                    existing.CarCategories.Add(new CarCategory { CarId = existing.Id, CategoryId = category.Id, Category = category, Car = existing });
                }
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
