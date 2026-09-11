using DVLD.Application.DTOs;
using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;

namespace DVLD.Application.Services;

public class ApplicationTypesService
    : IApplicationTypeService
{
    #region Fields

    private readonly IApplicationTypesRepository
        _applicationTypesRepository;

    #endregion


    #region Constructor

    public ApplicationTypesService(
        IApplicationTypesRepository applicationTypesRepository)
    {
        _applicationTypesRepository =
            applicationTypesRepository;
    }

    #endregion


    #region GET

    public async Task<ApplicationTypeDto?> GetByIdAsync(
        int applicationTypeId)
    {
        var applicationType =
            await _applicationTypesRepository
                .GetByIdAsync(applicationTypeId);

        return applicationType == null
            ? null
            : MapToDto(applicationType);
    }


    public async Task<List<ApplicationTypeDto>>
        GetAllAsync()
    {
        var applicationTypes =
            await _applicationTypesRepository
                .GetAllAsync();

        return applicationTypes
            .Select(MapToDto)
            .ToList();
    }

    public async Task<decimal?> GetApplicationFees(int appId)
    {
        if (appId<-1)
        {
            return null;
        }
        return await _applicationTypesRepository.GetApplicationFees(appId);
    }
    public async Task<int> CountAsync()
    {
        return await _applicationTypesRepository
            .CountAsync();
    }

    #endregion


    #region CREATE

    public async Task<int> CreateAsync(
        CreateApplicationTypeRequest request)
    {
        var applicationTypeExists =
            await _applicationTypesRepository
                .ApplicationTypeTitleExistsAsync(
                    request.ApplicationTypeTitle);

        if (applicationTypeExists)
            throw new InvalidOperationException(
                $"Application type '{request.ApplicationTypeTitle}' already exists.");

        var applicationType =
            new ApplicationType(
                request.ApplicationTypeTitle,
                request.ApplicationFees);

        await _applicationTypesRepository
            .AddAsync(applicationType);

        return applicationType.ApplicationTypeId;
    }

    #endregion


    #region UPDATE

    public async Task<bool> UpdateAsync(
        int applicationTypeId,
        UpdateApplicationTypeRequest request)
    {
        var applicationType =
            await _applicationTypesRepository
                .GetByIdAsync(applicationTypeId);

        if (applicationType == null)
            return false;

        var titleExists =
            await _applicationTypesRepository
                .ApplicationTypeTitleExistsAsync(
                    request.ApplicationTypeTitle);

        if (titleExists &&
            !string.Equals(
                applicationType.ApplicationTypeTitle,
                request.ApplicationTypeTitle,
                StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        try
        {
            applicationType.UpdateInformation(
                request.ApplicationTypeTitle,
                request.ApplicationFees);

            return await _applicationTypesRepository
                .UpdateAsync(applicationType);
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    #endregion


    #region DELETE

    public async Task<bool> DeleteAsync(
        int applicationTypeId)
    {
        var applicationType =
            await _applicationTypesRepository
                .GetByIdAsync(applicationTypeId);

        if (applicationType == null)
            return false;

        return await _applicationTypesRepository
            .DeleteAsync(applicationTypeId);
    }

    #endregion


    #region Mapping

    private static ApplicationTypeDto MapToDto(
        ApplicationType applicationType)
    {
        return new ApplicationTypeDto
        {
            ApplicationTypeId =
                applicationType.ApplicationTypeId,

            ApplicationTypeTitle =
                applicationType.ApplicationTypeTitle,

            ApplicationFees =
                applicationType.ApplicationFees
        };
    }

    #endregion
}