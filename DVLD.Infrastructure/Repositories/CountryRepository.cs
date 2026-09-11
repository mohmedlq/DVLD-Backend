using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Infrastructure.Data;
using DVLD.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DVLD.Infrastructure.Repositories;

public class CountriesRepository : ICountriesRepository
{
    private readonly DvldDbContext _context;

    public CountriesRepository(DvldDbContext context)
    {
        _context = context;
    }


    public async Task<Country?> GetByIdAsync(int countryId)
    {
        var entity = await _context.Countries
            .AsNoTracking()
            .FirstOrDefaultAsync(c =>
                c.CountryId == countryId);

        return entity == null
            ? null
            : EntityMapper.MapCountryToDomain(entity);
    }


    public async Task<List<Country>> GetAllAsync()
    {
        var entities = await _context.Countries
            .AsNoTracking()
            .ToListAsync();

        return entities
            .Select(EntityMapper.MapCountryToDomain)
            .ToList();
    }


    public async Task<bool> CountryNameExistsAsync(
        string countryName)
    {
        return await _context.Countries
            .AsNoTracking()
            .AnyAsync(c =>
                c.CountryName == countryName);
    }


    public async Task AddAsync(Country country)
    {
        var entity =
            EntityMapper.MapCountryToEntity(country);

        await _context.Countries.AddAsync(entity);

        await _context.SaveChangesAsync();

        country = EntityMapper.MapCountryToDomain(entity);
    }


    public async Task<bool> UpdateAsync(Country country)
    {
        var entity = await _context.Countries
            .FirstOrDefaultAsync(c =>
                c.CountryId == country.CountryId);

        if (entity == null)
            return false;

        entity.CountryName = country.CountryName;

        return await _context.SaveChangesAsync() > 0;
    }


    public async Task<bool> DeleteAsync(int countryId)
    {
        var entity = await _context.Countries
            .FirstOrDefaultAsync(c =>
                c.CountryId == countryId);

        if (entity == null)
            return false;

        _context.Countries.Remove(entity);

        return await _context.SaveChangesAsync() > 0;
    }


    public async Task<int> CountAsync()
    {
        return await _context.Countries
            .CountAsync();
    }
}