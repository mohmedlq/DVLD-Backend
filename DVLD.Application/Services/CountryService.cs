using DVLD.Application.DTOs;
using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;

namespace DVLD.Application.Services;

public class CountriesService : ICountryService
{
    private readonly ICountriesRepository _countriesRepository;

    public CountriesService(
        ICountriesRepository countriesRepository)
    {
        _countriesRepository = countriesRepository;
    }


    public async Task<CountryDto?> GetByIdAsync(int countryId)
    {
        var country = await _countriesRepository.GetByIdAsync(countryId);

        return country == null
            ? null
            : MapToDto(country);
    }


    public async Task<List<CountryDto>> GetAllAsync()
    {
        var countries = await _countriesRepository.GetAllAsync();

        return countries
            .Select(MapToDto)
            .ToList();
    }


    public async Task<int> CreateAsync(
        CreateCountryRequest request)
    {
        var countryExists =
            await _countriesRepository.CountryNameExistsAsync(
                request.CountryName);

        if (countryExists)
            throw new InvalidOperationException(
                $"Country '{request.CountryName}' already exists.");

        var country = new Country(
            request.CountryName);

        await _countriesRepository.AddAsync(country);

        return country.CountryId;
    }


    public async Task<bool> UpdateAsync(
        int countryId,
        UpdateCountryRequest request)
    {
        var country =
            await _countriesRepository.GetByIdAsync(countryId);

        if (country == null)
            return false;

        var countryExists =
            await _countriesRepository.CountryNameExistsAsync(
                request.CountryName);

        if (countryExists &&
            !string.Equals(
                country.CountryName,
                request.CountryName,
                StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        try
        {
            country.UpdateInformation(
                request.CountryName);

            return await _countriesRepository.UpdateAsync(country);
        }
        catch (ArgumentException)
        {
            return false;
        }
    }


    public async Task<bool> DeleteAsync(int countryId)
    {
        var country =
            await _countriesRepository.GetByIdAsync(countryId);

        if (country == null)
            return false;

        return await _countriesRepository.DeleteAsync(countryId);
    }


    public async Task<int> CountAsync()
    {
        return await _countriesRepository.CountAsync();
    }


    private static CountryDto MapToDto(
        Country country)
    {
        return new CountryDto
        {
            CountryId = country.CountryId,
            CountryName = country.CountryName
        };
    }
}