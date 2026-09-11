using DVLD.Application.DTOs;
using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
namespace DVLD.Application.Services;

public class DriversService : IDriverService
{
    private readonly IDriversRepository _driversRepository;
    private readonly IPeopleRepository _peopleRepository;
    private readonly IUsersRepository _usersRepository;

    public DriversService(
        IDriversRepository driversRepository,
        IPeopleRepository peopleRepository,
        IUsersRepository usersRepository)
    {
        _driversRepository = driversRepository;
        _peopleRepository = peopleRepository;
        _usersRepository = usersRepository;
    }

    public async Task<DriverDto?> GetByIdAsync(int driverId)
    {
        var driver = await _driversRepository.GetByIdAsync(driverId);

        return driver == null
            ? null
            : MapToDto(driver);
    }

    public async Task<DriverDto?> GetByPersonIdAsync(int personId)
    {
        var driver = await _driversRepository.GetByPersonIdAsync(personId);

        return driver == null
            ? null
            : MapToDto(driver);
    }

    public async Task<List<DriverDto>> GetAllAsync()
    {
        var drivers = await _driversRepository.GetAllAsync();

        return drivers
            .Select(MapToDto)
            .ToList();
    }

    public async Task<List<DriverDto>> GetByCreatedByUserIdAsync(int userId)
    {
        var drivers = await _driversRepository.GetByCreatedByUserIdAsync(userId);

        return drivers
            .Select( MapToDto)
            .ToList();
    }

    public async Task<int> CreateAsync(CreateDriverRequest request)
    {
        // Check if Person exists
        var person = await _peopleRepository.GetByIdAsync(request.PersonId);
        if (person == null)
            throw new KeyNotFoundException(
                $"Person with ID {request.PersonId} was not found.");

        // Check if User (CreatedByUserId) exists
        var user = await _usersRepository.GetByIdAsync(request.CreatedByUserId);
        if (user == null)
            throw new KeyNotFoundException(
                $"User with ID {request.CreatedByUserId} was not found.");

        // Check if Person already has a Driver record
        var driverExists = await _driversRepository.ExistsByPersonIdAsync(request.PersonId);
        if (driverExists)
            throw new InvalidOperationException(
                $"Person with ID {request.PersonId} already has a Driver record.");

        try
        {
            // Create Domain Driver
            var driver = new Driver(
                request.PersonId,
                request.CreatedByUserId,
                person,
                user);

            // Save
            await _driversRepository.AddAsync(driver);

            return driver.DriverId;
        }
        catch (ArgumentException ex)
        {
            throw new InvalidOperationException(
                $"Failed to create driver: {ex.Message}",
                ex);
        }
    }

    public async Task<bool> UpdateAsync(
        int driverId,
        UpdateDriverRequest request)
    {
        var driver = await _driversRepository.GetByIdAsync(driverId);

        if (driver == null)
            return false;

        // Check if Person exists
        var person = await _peopleRepository.GetByIdAsync(request.PersonId);
        if (person == null)
            return false;

        // Check if User (CreatedByUserId) exists
        var user = await _usersRepository.GetByIdAsync(request.CreatedByUserId);
        if (user == null)
            return false;

        // Check if another driver already has this PersonId
        if (request.PersonId != driver.PersonId)
        {
            var driverExists = await _driversRepository.ExistsByPersonIdAsync(request.PersonId);
            if (driverExists)
                return false;
        }

        try
        {
            var updatedDriver = new Driver(
                driverId,
                request.PersonId,
                request.CreatedByUserId,
                driver.CreatedDate,
                person,
                user);

            await _driversRepository.UpdateAsync(updatedDriver);

            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int driverId)
    {
        var driver = await _driversRepository.GetByIdAsync(driverId);

        if (driver == null)
            return false;

        return await _driversRepository.DeleteAsync(driverId);
    }

    public async Task<int> CountAsync()
    {
        return await _driversRepository.CountAsync();
    }


    private static DriverDto MapToDto(Driver driver)
    {
        return new DriverDto
        {
            DriverId = driver.DriverId,
            PersonId = driver.PersonId,
            CreatedByUserId = driver.CreatedByUserId,
            CreatedDate = driver.CreatedDate,
        };
    }

}