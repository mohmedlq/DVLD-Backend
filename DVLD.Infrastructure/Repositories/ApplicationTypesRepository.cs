using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Infrastructure.Data;
using DVLD.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DVLD.Infrastructure.Repositories;

public class ApplicationTypesRepository
    : IApplicationTypesRepository
{
    #region Fields

    private readonly DvldDbContext _context;

    #endregion


    #region Constructor

    public ApplicationTypesRepository(
        DvldDbContext context)
    {
        _context = context;
    }

    #endregion


    #region GET

    public async Task<ApplicationType?> GetByIdAsync(
        int applicationTypeId)
    {
        var entity =
            await _context.ApplicationTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(a =>
                    a.ApplicationTypeId ==
                    applicationTypeId);

        return entity == null
            ? null
            : DomainMapper.MapApplicationTypeToDomain(
                entity);
    }

    public async Task<decimal> GetApplicationFees(int AppId)
    {
        return GetByIdAsync(AppId).Result.ApplicationFees;
    }


    public async Task<List<ApplicationType>>
        GetAllAsync()
    {
        var entities =
            await _context.ApplicationTypes
                .AsNoTracking()
                .ToListAsync();

        return entities
            .Select(
                DomainMapper.MapApplicationTypeToDomain)
            .ToList();
    }


    public async Task<int> CountAsync()
    {
        return await _context.ApplicationTypes
            .CountAsync();
    }

    #endregion


    #region EXISTS

    public async Task<bool> ApplicationTypeTitleExistsAsync(
        string applicationTypeTitle)
    {
        return await _context.ApplicationTypes
            .AsNoTracking()
            .AnyAsync(a =>
                a.ApplicationTypeTitle ==
                applicationTypeTitle);
    }

    #endregion


    #region CREATE

    public async Task AddAsync(
        ApplicationType applicationType)
    {
        var entity =
            EntityMapper.MapApplicationTypeToEntity(
                applicationType);

        await _context.ApplicationTypes
            .AddAsync(entity);

        await _context.SaveChangesAsync();
    }

    #endregion


    #region UPDATE

    public async Task<bool> UpdateAsync(
        ApplicationType applicationType)
    {
        var entity =
            await _context.ApplicationTypes
                .FirstOrDefaultAsync(a =>
                    a.ApplicationTypeId ==
                    applicationType.ApplicationTypeId);

        if (entity == null)
            return false;

        entity.ApplicationTypeTitle =
            applicationType.ApplicationTypeTitle;

        entity.ApplicationFees =
            applicationType.ApplicationFees;

        return await _context.SaveChangesAsync() > 0;
    }

    #endregion


    #region DELETE

    public async Task<bool> DeleteAsync(
        int applicationTypeId)
    {
        var entity =
            await _context.ApplicationTypes
                .FirstOrDefaultAsync(a =>
                    a.ApplicationTypeId ==
                    applicationTypeId);

        if (entity == null)
            return false;

        _context.ApplicationTypes.Remove(entity);

        return await _context.SaveChangesAsync() > 0;
    }

    #endregion
}