using DVLD.Application.DTOs;
using DVLD.Domain.Entities;

namespace DVLD.Application.Interfaces;

public interface IDriversRepository : ICrud<Driver>
{
    Task<Driver?> GetByPersonIdAsync(int personId);

    Task<bool> ExistsByPersonIdAsync(int personId);

    Task<int> CountAsync();

    Task<List<Driver>> GetByCreatedByUserIdAsync(int userId);
}

public interface IDriverService
{
    Task<DriverDto?> GetByIdAsync(int driverId);

    Task<DriverDto?> GetByPersonIdAsync(int personId);

    Task<List<DriverDto>> GetAllAsync();

    Task<List<DriverDto>> GetByCreatedByUserIdAsync(int userId);

    Task<int> CreateAsync(CreateDriverRequest request);

    Task<bool> UpdateAsync(int driverId, UpdateDriverRequest request);

    Task<bool> DeleteAsync(int driverId);

    Task<int> CountAsync();
}