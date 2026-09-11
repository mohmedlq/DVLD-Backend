using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DVLD.Infrastructure.Repositories;

public class LicenseClassRepository : ILicenseClassRepository
{
    private readonly DvldDbContext _context;

    public LicenseClassRepository(DvldDbContext context)
    {
        _context = context;
    }

    #region Get
    public async Task<LicenseClass?> GetByIdAsync(int licenseClassId)
    {
        var entity = await _context.LicenseClasses
            .AsNoTracking()
            .FirstOrDefaultAsync(lc => lc.LicenseClassId == licenseClassId);

        return entity == null
            ? null
            : MapToDomain(entity);
    }

    public async Task<LicenseClass?> GetByNameAsync(string className)
    {
        if (string.IsNullOrWhiteSpace(className))
            return null;

        var entity = await _context.LicenseClasses
            .AsNoTracking()
            .FirstOrDefaultAsync(lc => lc.ClassName == className);

        return entity == null
            ? null
            : MapToDomain(entity);
    }

    public async Task<List<LicenseClass>> GetAllAsync()
    {
        var entities = await _context.LicenseClasses
            .AsNoTracking()
            .ToListAsync();

        return entities
            .Select(MapToDomain)
            .ToList();
    }
    #endregion

    #region Check
    public async Task<bool> ExistsAsync(int licenseClassId)
    {
        return await _context.LicenseClasses
            .AsNoTracking()
            .AnyAsync(lc => lc.LicenseClassId == licenseClassId);
    }

    public async Task<bool> NameExistsAsync(string className)
    {
        if (string.IsNullOrWhiteSpace(className))
            return false;

        return await _context.LicenseClasses
            .AsNoTracking()
            .AnyAsync(lc => lc.ClassName == className);
    }

    public async Task<int> CountAsync()
    {
        return await _context.LicenseClasses
            .AsNoTracking()
            .CountAsync();
    }
    #endregion

    #region Create
    public async Task AddAsync(LicenseClass licenseClass)
    {
        var entity = MapToEntity(licenseClass);

        await _context.LicenseClasses.AddAsync(entity);

        await _context.SaveChangesAsync();
    }
    #endregion

    #region Update
    public async Task<bool> UpdateAsync(int licenseClassId, LicenseClass licenseClass)
    {
        var entity = await _context.LicenseClasses
            .FirstOrDefaultAsync(lc => lc.LicenseClassId == licenseClassId);

        if (entity == null)
            return false;

        entity.ClassName = licenseClass.ClassName;
        entity.ClassDescription = licenseClass.ClassDescription;
        entity.ClassFees = licenseClass.ClassFees;
        entity.MinimumAllowedAge = licenseClass.MinimumAllowedAge;
        entity.DefaultValidityLength = licenseClass.DefaultValidityLength;

        return await _context.SaveChangesAsync() > 0;
    }
    #endregion

    #region Delete
    public async Task<bool> DeleteAsync(int licenseClassId)
    {
        var entity = await _context.LicenseClasses
            .FirstOrDefaultAsync(lc => lc.LicenseClassId == licenseClassId);

        if (entity == null)
            return false;

        _context.LicenseClasses.Remove(entity);

        return await _context.SaveChangesAsync() > 0;
    }
    #endregion

    #region Mapping
    private static LicenseClass MapToDomain(Infrastructure.Entities.LicenseClass entity)
    {
        return new LicenseClass(
            entity.LicenseClassId,
            entity.ClassName,
            entity.ClassDescription,
            entity.ClassFees,
            entity.MinimumAllowedAge,
            entity.DefaultValidityLength);
    }

    private static Infrastructure.Entities.LicenseClass MapToEntity(LicenseClass domain)
    {
        return new Infrastructure.Entities.LicenseClass
        {
            LicenseClassId = domain.LicenseClassId,
            ClassName = domain.ClassName,
            ClassDescription = domain.ClassDescription,
            ClassFees = domain.ClassFees,
            MinimumAllowedAge = domain.MinimumAllowedAge,
            DefaultValidityLength = domain.DefaultValidityLength
        };
    }
    #endregion
}