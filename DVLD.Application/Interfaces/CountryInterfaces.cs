using DVLD.Application.DTOs;
using DVLD.Domain.Entities;

namespace DVLD.Application.Interfaces;

public interface ICountriesRepository : ICrud<Country>
{
    Task<bool> CountryNameExistsAsync(string countryName);

    Task<int> CountAsync();
}


public interface ICountryService
{
    Task<CountryDto?> GetByIdAsync(int countryId);

    Task<List<CountryDto>> GetAllAsync();

    Task<int> CreateAsync(CreateCountryRequest request);

    Task<bool> UpdateAsync(
        int countryId,
        UpdateCountryRequest request);

    Task<bool> DeleteAsync(int countryId);

    Task<int> CountAsync();
}