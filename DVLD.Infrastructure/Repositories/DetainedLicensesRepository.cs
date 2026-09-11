using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Infrastructure.Data;
using DVLD.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DVLD.Infrastructure.Repositories;

public class DetainedLicensesRepository
    : IDetainedLicensesRepository
{
    #region Fields

    private readonly DvldDbContext _context;

    #endregion


    #region Constructor

    public DetainedLicensesRepository(
        DvldDbContext context)
    {
        _context = context;
    }

    #endregion


    #region Get

    public async Task<DetainedLicense?> GetByIdAsync(
        int detainId)
    {
        var entity = await _context.DetainedLicenses
            .AsNoTracking()
            .FirstOrDefaultAsync(d =>
                d.DetainId == detainId);

        return entity == null
            ? null
            : EntityMapper.MapDetainedLicenseToDomain(
                entity);
    }


    public async Task<List<DetainedLicense>> GetAllAsync()
    {
        var entities = await _context.DetainedLicenses
            .AsNoTracking()
            .ToListAsync();

        return entities
            .Select(
                EntityMapper.MapDetainedLicenseToDomain)
            .ToList();
    }


    public async Task<List<DetainedLicense>>
        GetByLicenseIdAsync(int licenseId)
    {
        var entities = await _context.DetainedLicenses
            .AsNoTracking()
            .Where(d =>
                d.LicenseId == licenseId)
            .ToListAsync();

        return entities
            .Select(
                EntityMapper.MapDetainedLicenseToDomain)
            .ToList();
    }


    public async Task<List<DetainedLicense>>
        GetByCreatedByUserIdAsync(int userId)
    {
        var entities = await _context.DetainedLicenses
            .AsNoTracking()
            .Where(d =>
                d.CreatedByUserId == userId)
            .ToListAsync();

        return entities
            .Select(
                EntityMapper.MapDetainedLicenseToDomain)
            .ToList();
    }


    public async Task<List<DetainedLicense>>
        GetReleasedAsync()
    {
        var entities = await _context.DetainedLicenses
            .AsNoTracking()
            .Where(d =>
                d.IsReleased)
            .ToListAsync();

        return entities
            .Select(
                EntityMapper.MapDetainedLicenseToDomain)
            .ToList();
    }


    public async Task<List<DetainedLicense>>
        GetUnreleasedAsync()
    {
        var entities = await _context.DetainedLicenses
            .AsNoTracking()
            .Where(d =>
                !d.IsReleased)
            .ToListAsync();

        return entities
            .Select(
                EntityMapper.MapDetainedLicenseToDomain)
            .ToList();
    }

    #endregion


    #region Exists

    public async Task<bool> ExistsByLicenseIdAsync(
        int licenseId)
    {
        return await _context.DetainedLicenses
            .AsNoTracking()
            .AnyAsync(d =>
                d.LicenseId == licenseId &&
                !d.IsReleased);
    }

    #endregion


    #region Create

    public async Task AddAsync(
        DetainedLicense detainedLicense)
    {
        var entity =
            EntityMapper.MapDetainedLicenseToEntity(
                detainedLicense);

        await _context.DetainedLicenses
            .AddAsync(entity);

        await _context.SaveChangesAsync();
    }

    #endregion


    #region Update

    public async Task<bool> UpdateAsync(
        DetainedLicense detainedLicense)
    {
        var entity = await _context.DetainedLicenses
            .FirstOrDefaultAsync(d =>
                d.DetainId ==
                detainedLicense.DetainId);

        if (entity == null)
            return false;


        entity.FineFees =
            detainedLicense.FineFees;

        entity.IsReleased =
            detainedLicense.IsReleased;

        entity.ReleaseDate =
            detainedLicense.ReleaseDate;

        entity.ReleasedByUserId =
            detainedLicense.ReleasedByUserId;

        entity.ReleaseApplicationId =
            detainedLicense.ReleaseApplicationId;


        return await _context.SaveChangesAsync() > 0;
    }

    #endregion


    #region Delete

    public async Task<bool> DeleteAsync(
        int detainId)
    {
        var entity = await _context.DetainedLicenses
            .FirstOrDefaultAsync(d =>
                d.DetainId == detainId);

        if (entity == null)
            return false;

        _context.DetainedLicenses.Remove(entity);

        return await _context.SaveChangesAsync() > 0;
    }

    #endregion


    #region Count

    public async Task<int> CountAsync()
    {
        return await _context.DetainedLicenses
            .CountAsync();
    }

    #endregion
}