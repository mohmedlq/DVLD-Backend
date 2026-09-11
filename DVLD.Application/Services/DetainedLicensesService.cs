using DVLD.Application.DTOs;
using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;

namespace DVLD.Application.Services;

public class DetainedLicensesService
    : IDetainedLicenseService
{
    #region Fields

    private readonly IDetainedLicensesRepository
        _detainedLicensesRepository;

    private readonly IUsersRepository
        _usersRepository;

    #endregion


    #region Constructor

    public DetainedLicensesService(
        IDetainedLicensesRepository detainedLicensesRepository,
        IUsersRepository usersRepository)
    {
        _detainedLicensesRepository =
            detainedLicensesRepository;

        _usersRepository =
            usersRepository;
    }

    #endregion


    #region Get

    public async Task<DetainedLicenseDto?> GetByIdAsync(
        int detainId)
    {
        var detainedLicense =
            await _detainedLicensesRepository
                .GetByIdAsync(detainId);

        return detainedLicense == null
            ? null
            : MapToDto(detainedLicense);
    }


    public async Task<List<DetainedLicenseDto>> GetAllAsync()
    {
        var detainedLicenses =
            await _detainedLicensesRepository.GetAllAsync();

        return detainedLicenses
            .Select(MapToDto)
            .ToList();
    }


    public async Task<List<DetainedLicenseDto>>
        GetByLicenseIdAsync(int licenseId)
    {
        var detainedLicenses =
            await _detainedLicensesRepository
                .GetByLicenseIdAsync(licenseId);

        return detainedLicenses
            .Select(MapToDto)
            .ToList();
    }


    public async Task<List<DetainedLicenseDto>>
        GetByCreatedByUserIdAsync(int userId)
    {
        var detainedLicenses =
            await _detainedLicensesRepository
                .GetByCreatedByUserIdAsync(userId);

        return detainedLicenses
            .Select(MapToDto)
            .ToList();
    }


    public async Task<List<DetainedLicenseDto>>
        GetReleasedAsync()
    {
        var detainedLicenses =
            await _detainedLicensesRepository
                .GetReleasedAsync();

        return detainedLicenses
            .Select(MapToDto)
            .ToList();
    }


    public async Task<List<DetainedLicenseDto>>
        GetUnreleasedAsync()
    {
        var detainedLicenses =
            await _detainedLicensesRepository
                .GetUnreleasedAsync();

        return detainedLicenses
            .Select(MapToDto)
            .ToList();
    }

    #endregion


    #region Create

    public async Task<int> CreateAsync(
        CreateDetainedLicenseRequest request)
    {
        var user =
            await _usersRepository
                .GetByIdAsync(request.CreatedByUserId);

        if (user == null)
            throw new KeyNotFoundException(
                $"User with ID {request.CreatedByUserId} was not found.");


        var alreadyDetained =
            await _detainedLicensesRepository
                .ExistsByLicenseIdAsync(request.LicenseId);

        if (alreadyDetained)
            throw new InvalidOperationException(
                $"License with ID {request.LicenseId} is already detained.");


        var detainedLicense = new DetainedLicense(
            request.LicenseId,
            request.FineFees,
            request.CreatedByUserId);


        await _detainedLicensesRepository
            .AddAsync(detainedLicense);


        return detainedLicense.DetainId;
    }

    #endregion


    #region Update

    public async Task<bool> UpdateAsync(
        int detainId,
        UpdateDetainedLicenseRequest request)
    {
        var detainedLicense =
            await _detainedLicensesRepository
                .GetByIdAsync(detainId);

        if (detainedLicense == null)
            return false;

        if (detainedLicense.IsReleased)
            return false;


        try
        {
            detainedLicense.UpdateInformation(
                request.FineFees);

            return await _detainedLicensesRepository
                .UpdateAsync(detainedLicense);
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    #endregion


    #region Release

    public async Task<bool> ReleaseAsync(
        int detainId,
        ReleaseDetainedLicenseRequest request)
    {
        var detainedLicense =
            await _detainedLicensesRepository
                .GetByIdAsync(detainId);

        if (detainedLicense == null)
            return false;


        var user =
            await _usersRepository
                .GetByIdAsync(request.ReleasedByUserId);

        if (user == null)
            return false;


        try
        {
            detainedLicense.Release(
                request.ReleasedByUserId,
                request.ReleaseApplicationId);

            return await _detainedLicensesRepository
                .UpdateAsync(detainedLicense);
        }
        catch (InvalidOperationException)
        {
            return false;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    #endregion


    #region Delete

    public async Task<bool> DeleteAsync(
        int detainId)
    {
        var detainedLicense =
            await _detainedLicensesRepository
                .GetByIdAsync(detainId);

        if (detainedLicense == null)
            return false;

        return await _detainedLicensesRepository
            .DeleteAsync(detainId);
    }

    #endregion


    #region Count

    public async Task<int> CountAsync()
    {
        return await _detainedLicensesRepository
            .CountAsync();
    }

    #endregion


    #region Mapping

    private static DetainedLicenseDto MapToDto(
        DetainedLicense detainedLicense)
    {
        return new DetainedLicenseDto
        {
            DetainId = detainedLicense.DetainId,
            LicenseId = detainedLicense.LicenseId,
            DetainDate = detainedLicense.DetainDate,
            FineFees = detainedLicense.FineFees,
            CreatedByUserId =
                detainedLicense.CreatedByUserId,
            IsReleased =
                detainedLicense.IsReleased,
            ReleaseDate =
                detainedLicense.ReleaseDate,
            ReleasedByUserId =
                detainedLicense.ReleasedByUserId,
            ReleaseApplicationId =
                detainedLicense.ReleaseApplicationId
        };
    }

    #endregion
}