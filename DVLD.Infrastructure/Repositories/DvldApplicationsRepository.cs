using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Infrastructure.Data;
using DVLD.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DVLD.Infrastructure.Repositories;

public class DvldApplicationsRepository
    : IDvldApplicationsRepository
{
    #region Fields

    private readonly DvldDbContext _context;

    #endregion


    #region Constructor

    public DvldApplicationsRepository(
        DvldDbContext context)
    {
        _context = context;
    }

    #endregion


    #region GET

    public async Task<DvldApplication?> GetByIdAsync(
        int applicationId)
    {
        var entity =
            await _context.Applications
                .AsNoTracking()
                .FirstOrDefaultAsync(a =>
                    a.ApplicationId ==
                    applicationId);

        return entity == null
            ? null
            : EntityMapper.MapDvldApplicationToDomain(
                entity);
    }


    public async Task<List<DvldApplication>>
        GetAllAsync()
    {
        var entities =
            await _context.Applications
                .AsNoTracking()
                .ToListAsync();

        return entities
            .Select(
                EntityMapper.MapDvldApplicationToDomain)
            .ToList();
    }


    public async Task<List<DvldApplication>>
        GetByApplicantPersonIdAsync(
            int personId)
    {
        var entities =
            await _context.Applications
                .AsNoTracking()
                .Where(a =>
                    a.ApplicantPersonId ==
                    personId)
                .ToListAsync();

        return entities
            .Select(
                EntityMapper.MapDvldApplicationToDomain)
            .ToList();
    }


    public async Task<List<DvldApplication>>
        GetByApplicationTypeIdAsync(
            int applicationTypeId)
    {
        var entities =
            await _context.Applications
                .AsNoTracking()
                .Where(a =>
                    a.ApplicationTypeId ==
                    applicationTypeId)
                .ToListAsync();

        return entities
            .Select(
                EntityMapper.MapDvldApplicationToDomain)
            .ToList();
    }


    public async Task<List<DvldApplication>>
        GetByCreatedByUserIdAsync(
            int userId)
    {
        var entities =
            await _context.Applications
                .AsNoTracking()
                .Where(a =>
                    a.CreatedByUserId ==
                    userId)
                .ToListAsync();

        return entities
            .Select(
                EntityMapper.MapDvldApplicationToDomain)
            .ToList();
    }


    public async Task<List<DvldApplication>>
        GetByStatusAsync(
            byte applicationStatus)
    {
        var entities =
            await _context.Applications
                .AsNoTracking()
                .Where(a =>
                    a.ApplicationStatus ==
                    applicationStatus)
                .ToListAsync();

        return entities
            .Select(
                EntityMapper.MapDvldApplicationToDomain)
            .ToList();
    }


    public async Task<int> CountAsync()
    {
        return await _context.Applications
            .CountAsync();
    }

    #endregion


    #region EXISTS

    public async Task<bool> ExistsAsync(
        int applicantPersonId,
        int applicationTypeId)
    {
        return await _context.Applications
            .AsNoTracking()
            .AnyAsync(a =>
                a.ApplicantPersonId ==
                applicantPersonId &&
                a.ApplicationTypeId ==
                applicationTypeId);
    }

    #endregion


    #region CREATE

    public async Task AddAsync(
        DvldApplication application)
    {
        var entity =
            EntityMapper.MapDvldApplicationToEntity(
                application);

        await _context.Applications
            .AddAsync(entity);

        await _context.SaveChangesAsync();
    }

    #endregion


    #region UPDATE

    public async Task<bool> UpdateAsync(
        DvldApplication application)
    {
        var entity =
            await _context.Applications
                .FirstOrDefaultAsync(a =>
                    a.ApplicationId ==
                    application.ApplicationId);

        if (entity == null)
            return false;


        entity.ApplicationTypeId =
            application.ApplicationTypeId;

        entity.ApplicationStatus =
            application.ApplicationStatus;

        entity.LastStatusDate =
            application.LastStatusDate;

        entity.PaidFees =
            application.PaidFees;

        return await _context.SaveChangesAsync() > 0;
    }

    #endregion


    #region DELETE

    public async Task<bool> DeleteAsync(
        int applicationId)
    {
        var entity =
            await _context.Applications
                .FirstOrDefaultAsync(a =>
                    a.ApplicationId ==
                    applicationId);

        if (entity == null)
            return false;

        _context.Applications.Remove(entity);

        return await _context.SaveChangesAsync() > 0;
    }

    #endregion
}