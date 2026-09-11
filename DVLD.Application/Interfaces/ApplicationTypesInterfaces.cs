using DVLD.Application.DTOs;
using DVLD.Domain.Entities;

namespace DVLD.Application.Interfaces;

public interface IApplicationTypesRepository
    : ICrud<ApplicationType>
{
     Task<decimal> GetApplicationFees(int AppId);

    Task<bool> ApplicationTypeTitleExistsAsync(
        string applicationTypeTitle);

    Task<int> CountAsync();

}
public interface IApplicationTypeService
{

    Task<ApplicationTypeDto?> GetByIdAsync(
        int applicationTypeId);

    Task<List<ApplicationTypeDto>> GetAllAsync();

    Task<int> CreateAsync(
        CreateApplicationTypeRequest request);

    Task<bool> UpdateAsync(
        int applicationTypeId,
        UpdateApplicationTypeRequest request);

    Task<bool> DeleteAsync(
        int applicationTypeId);

    Task<int> CountAsync();

}
