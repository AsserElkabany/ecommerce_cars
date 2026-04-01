using WebApplication1.DTOs;

namespace WebApplication1.services
{
    public interface ICarService
    {
        Task<CarReadDto> CreateCarAsync(CarCreateDto dto);
        Task<IEnumerable<CarReadDto>> GetAllCarsAsync();
        Task<CarReadDto?> GetCarByIdAsync(int id);
        Task<bool> UpdateCarAsync(int id, CarUpdateDto dto);
        Task<bool> AssignCarAsync(int id, int sellerUserId);
        Task<bool> DeleteCarAsync(int id);
        Task<bool> SetCarAvailabilityAsync(int id, bool isAvailable);
    }
}
