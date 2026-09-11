using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Infrastructure.Data;
using DVLD.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using System;

namespace DVLD.Infrastructure.Repositories;

public class DriversRepository : IDriversRepository
{
    private readonly DvldDbContext _context;

    public DriversRepository(DvldDbContext context)
    {
        _context = context;
    }

    public async Task<Driver?> GetByIdAsync(int driverId)
    {
        var entity = await _context.Drivers
            .AsNoTracking()
            .Include(d => d.Person)
            .Include(d => d.CreatedByUser)
                        .ThenInclude(u => u.Person)
            .FirstOrDefaultAsync(d => d.DriverId == driverId);

        return entity == null
            ? null
            : DomainMapper.MapToDomain(entity);
    }

    public async Task<Driver?> GetByPersonIdAsync(int personId)
    {
        var entity = await _context.Drivers
            .AsNoTracking()
            .Include(d => d.Person)
            .Include(d => d.CreatedByUser)
                        .ThenInclude(u => u.Person)
            .FirstOrDefaultAsync(d => d.PersonId == personId);

        return entity == null
            ? null
            : DomainMapper.MapToDomain(entity);
    }

    public async Task<bool> ExistsByPersonIdAsync(int personId)
    {
        return await _context.Drivers
            .AsNoTracking()
            .AnyAsync(d => d.PersonId == personId);
    }

    public async Task<int> CountAsync()
    {
        return await _context.Drivers
            .AsNoTracking()
            .CountAsync();
    }

    public async Task<List<Driver>> GetAllAsync()
    {
        var entities = await _context.Drivers
            .AsNoTracking()
            .Include(d => d.Person)
            .Include(d => d.CreatedByUser)
            .ThenInclude(u => u.Person)
            .ToListAsync();

        return entities
            .Select(DomainMapper.MapToDomain)
            .ToList();
    }

    public async Task<List<Driver>> GetByCreatedByUserIdAsync(int userId)
    {
        var entities = await _context.Drivers
            .AsNoTracking()
            .Include(d => d.Person)
            .Include(d => d.CreatedByUser)
                        .ThenInclude(u => u.Person)
            .Where(d => d.CreatedByUserId == userId)
            .ToListAsync();

        return entities
            .Select(DomainMapper.MapToDomain)
            .ToList();
    }

    public async Task AddAsync(Driver entity)
    {
        var driver = EntityMapper.MapDriverToEntity(entity);

        await _context.Drivers.AddAsync(driver);

        await _context.SaveChangesAsync();
    }
    
    public async Task<bool> UpdateAsync(Driver entity)
    {
        var driver = await GetEntityByIdAsync(entity.DriverId);

        if (driver == null)
            throw new KeyNotFoundException(
                $"Driver with ID {entity.DriverId} was not found.");

        driver.PersonId = entity.PersonId;
        driver.CreatedByUserId = entity.CreatedByUserId;

       return await _context.SaveChangesAsync()>0;
    }

    public async Task<bool> DeleteAsync(int driverId)
    {
        var driver = await GetEntityByIdAsync(driverId);

        if (driver == null)
            return false;

        _context.Drivers.Remove(driver);

        return await _context.SaveChangesAsync() > 0;
    }

    private async Task<Infrastructure.Entities.Driver?> GetEntityByIdAsync(
        int driverId)
    {
        return await _context.Drivers
            .FirstOrDefaultAsync(d => d.DriverId == driverId);
    }



   
}
