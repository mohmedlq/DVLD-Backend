using DVLD.Application.DTOs;
using DVLD.Domain.Entities;

namespace DVLD.Application.Interfaces;

public interface IDetainedLicensesRepository
    : ICrud<DetainedLicense>
{
    #region Methods

    Task<List<DetainedLicense>> GetByLicenseIdAsync(
        int licenseId);

    Task<List<DetainedLicense>> GetByCreatedByUserIdAsync(
        int userId);

    Task<List<DetainedLicense>> GetReleasedAsync();

    Task<List<DetainedLicense>> GetUnreleasedAsync();

    Task<bool> ExistsByLicenseIdAsync(
        int licenseId);

    Task<int> CountAsync();

    #endregion
}


public interface IDetainedLicenseService
{
    #region Methods

    Task<DetainedLicenseDto?> GetByIdAsync(
        int detainId);

    Task<List<DetainedLicenseDto>> GetAllAsync();

    Task<List<DetainedLicenseDto>> GetByLicenseIdAsync(
        int licenseId);

    Task<List<DetainedLicenseDto>> GetByCreatedByUserIdAsync(
        int userId);

    Task<List<DetainedLicenseDto>> GetReleasedAsync();

    Task<List<DetainedLicenseDto>> GetUnreleasedAsync();

    Task<int> CreateAsync(
        CreateDetainedLicenseRequest request);

    Task<bool> UpdateAsync(
        int detainId,
        UpdateDetainedLicenseRequest request);

    Task<bool> ReleaseAsync(
        int detainId,
        ReleaseDetainedLicenseRequest request);

    Task<bool> DeleteAsync(
        int detainId);

    Task<int> CountAsync();

    #endregion
}