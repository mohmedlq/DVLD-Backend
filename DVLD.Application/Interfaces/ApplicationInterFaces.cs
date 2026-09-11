using DVLD.Application.DTOs;
using DVLD.Domain.Entities;

namespace DVLD.Application.Interfaces;

public interface IDvldApplicationsRepository
    : ICrud<DvldApplication>
{

    Task<List<DvldApplication>>
        GetByApplicantPersonIdAsync(
            int personId);

    Task<List<DvldApplication>>
        GetByApplicationTypeIdAsync(
            int applicationTypeId);

    Task<List<DvldApplication>>
        GetByCreatedByUserIdAsync(
            int userId);

    Task<List<DvldApplication>>
        GetByStatusAsync(
            byte applicationStatus);

    Task<bool> ExistsAsync(
        int applicantPersonId,
        int applicationTypeId);

    Task<int> CountAsync();

}
public interface IDvldApplicationService
{

    Task<DvldApplicationDto?> GetByIdAsync(
        int applicationId);

    Task<List<DvldApplicationDto>> GetAllAsync();

    Task<List<DvldApplicationDto>>
        GetByApplicantPersonIdAsync(
            int personId);

    Task<List<DvldApplicationDto>>
        GetByApplicationTypeIdAsync(
            int applicationTypeId);

    Task<List<DvldApplicationDto>>
        GetByCreatedByUserIdAsync(
            int userId);

    Task<List<DvldApplicationDto>>
        GetByStatusAsync(
            byte applicationStatus);

    Task<int> CreateAsync(
        CreateDvldApplicationRequest request);

    Task<bool> UpdateAsync(
        int applicationId,
        UpdateDvldApplicationRequest request);

    Task<bool> ChangeStatusAsync(
        int applicationId,
        ChangeApplicationStatusRequest request);

    Task<bool> DeleteAsync(
        int applicationId);

    Task<int> CountAsync();

}