using DVLD.Application.DTOs;
using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;

namespace DVLD.Application.Services;

public class DvldApplicationsService
    : IDvldApplicationService
{
    #region Fields

    private readonly IDvldApplicationsRepository
        _applicationsRepository;

    private readonly IPeopleRepository
        _peopleRepository;

    private readonly IApplicationTypesRepository
        _applicationTypesRepository;

    private readonly IUsersRepository
        _usersRepository;

    #endregion


    #region Constructor

    public DvldApplicationsService(
        IDvldApplicationsRepository applicationsRepository,
        IPeopleRepository peopleRepository,
        IApplicationTypesRepository applicationTypesRepository,
        IUsersRepository usersRepository)
    {
        _applicationsRepository =
            applicationsRepository;

        _peopleRepository =
            peopleRepository;

        _applicationTypesRepository =
            applicationTypesRepository;

        _usersRepository =
            usersRepository;
    }

    #endregion


    #region GET

    public async Task<DvldApplicationDto?> GetByIdAsync(
        int applicationId)
    {
        var application =
            await _applicationsRepository
                .GetByIdAsync(applicationId);

        return application == null
            ? null
            : MapToDto(application);
    }


    public async Task<List<DvldApplicationDto>>
        GetAllAsync()
    {
        var applications =
            await _applicationsRepository
                .GetAllAsync();

        return applications
            .Select(MapToDto)
            .ToList();
    }


    public async Task<List<DvldApplicationDto>>
        GetByApplicantPersonIdAsync(
            int personId)
    {
        var applications =
            await _applicationsRepository
                .GetByApplicantPersonIdAsync(personId);

        return applications
            .Select(MapToDto)
            .ToList();
    }


    public async Task<List<DvldApplicationDto>>
        GetByApplicationTypeIdAsync(
            int applicationTypeId)
    {
        var applications =
            await _applicationsRepository
                .GetByApplicationTypeIdAsync(
                    applicationTypeId);

        return applications
            .Select(MapToDto)
            .ToList();
    }


    public async Task<List<DvldApplicationDto>>
        GetByCreatedByUserIdAsync(
            int userId)
    {
        var applications =
            await _applicationsRepository
                .GetByCreatedByUserIdAsync(userId);

        return applications
            .Select(MapToDto)
            .ToList();
    }


    public async Task<List<DvldApplicationDto>>
        GetByStatusAsync(
            byte applicationStatus)
    {
        var applications =
            await _applicationsRepository
                .GetByStatusAsync(applicationStatus);

        return applications
            .Select(MapToDto)
            .ToList();
    }


    public async Task<int> CountAsync()
    {
        return await _applicationsRepository
            .CountAsync();
    }

    #endregion


    #region CREATE

    public async Task<int> CreateAsync(
        CreateDvldApplicationRequest request)
    {
        var person =
            await _peopleRepository
                .GetByIdAsync(request.ApplicantPersonId);

        if (person == null)
            throw new KeyNotFoundException(
                $"Person with ID {request.ApplicantPersonId} was not found.");


        var applicationType =
            await _applicationTypesRepository
                .GetByIdAsync(request.ApplicationTypeId);

        if (applicationType == null)
            throw new KeyNotFoundException(
                $"Application type with ID {request.ApplicationTypeId} was not found.");


        var user =
            await _usersRepository
                .GetByIdAsync(request.CreatedByUserId);

        if (user == null)
            throw new KeyNotFoundException(
                $"User with ID {request.CreatedByUserId} was not found.");


        var application =
            new DvldApplication(
                request.ApplicantPersonId,
                request.ApplicationTypeId,
                request.ApplicationStatus,
                _applicationTypesRepository.GetApplicationFees(request.ApplicationTypeId).Result,
                request.CreatedByUserId);


        await _applicationsRepository
            .AddAsync(application);

        return application.ApplicationId;
    }

    #endregion


    #region UPDATE

    public async Task<bool> UpdateAsync(
        int applicationId,
        UpdateDvldApplicationRequest request)
    {
        var application =
            await _applicationsRepository
                .GetByIdAsync(applicationId);

        if (application == null)
            return false;


        var applicationType =
            await _applicationTypesRepository
                .GetByIdAsync(request.ApplicationTypeId);

        if (applicationType == null)
            return false;


        try
        {
            application.UpdateInformation(
                request.ApplicationTypeId,
                request.PaidFees);

            return await _applicationsRepository
                .UpdateAsync(application);
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    #endregion


    #region CHANGE STATUS

    public async Task<bool> ChangeStatusAsync(
        int applicationId,
        ChangeApplicationStatusRequest request)
    {
        var application =
            await _applicationsRepository
                .GetByIdAsync(applicationId);

        if (application == null)
            return false;


        try
        {
            application.ChangeStatus(
                request.ApplicationStatus);

            return await _applicationsRepository
                .UpdateAsync(application);
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    #endregion


    #region DELETE

    public async Task<bool> DeleteAsync(
        int applicationId)
    {
        var application =
            await _applicationsRepository
                .GetByIdAsync(applicationId);

        if (application == null)
            return false;

        return await _applicationsRepository
            .DeleteAsync(applicationId);
    }

    #endregion


    #region Mapping

    private static DvldApplicationDto MapToDto(
        DvldApplication application)
    {
        return new DvldApplicationDto
        {
            ApplicationId =
                application.ApplicationId,

            ApplicantPersonId =
                application.ApplicantPersonId,

            ApplicationDate =
                application.ApplicationDate,

            ApplicationTypeId =
                application.ApplicationTypeId,

            ApplicationStatus =
                application.ApplicationStatus,

            LastStatusDate =
                application.LastStatusDate,

            PaidFees =
                application.PaidFees,

            CreatedByUserId =
                application.CreatedByUserId
        };
    }

    #endregion
}