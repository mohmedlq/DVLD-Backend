using DVLD.Application.DTOs;
using DVLD.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.API.Controllers;

[ApiController]
[Route("api/Applications")]
public class DvldApplicationsController
    : ControllerBase
{
    #region Fields

    private readonly IDvldApplicationService
        _applicationService;

    #endregion


    #region Constructor

    public DvldApplicationsController(
        IDvldApplicationService applicationService)
    {
        _applicationService =
            applicationService;
    }

    #endregion


    #region GET

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var applications =
            await _applicationService
                .GetAllAsync();

        return Ok(applications);
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id)
    {
        var application =
            await _applicationService
                .GetByIdAsync(id);

        if (application == null)
            return NotFound();

        return Ok(application);
    }


    [HttpGet("Person/{personId:int}")]
    public async Task<IActionResult>
        GetByApplicantPersonId(
            int personId)
    {
        var applications =
            await _applicationService
                .GetByApplicantPersonIdAsync(
                    personId);

        return Ok(applications);
    }


    [HttpGet("Type/{applicationTypeId:int}")]
    public async Task<IActionResult>
        GetByApplicationTypeId(
            int applicationTypeId)
    {
        var applications =
            await _applicationService
                .GetByApplicationTypeIdAsync(
                    applicationTypeId);

        return Ok(applications);
    }


    [HttpGet("CreatedBy/{userId:int}")]
    public async Task<IActionResult>
        GetByCreatedByUserId(
            int userId)
    {
        var applications =
            await _applicationService
                .GetByCreatedByUserIdAsync(
                    userId);

        return Ok(applications);
    }


    [HttpGet("Status/{status:int}")]
    public async Task<IActionResult>
        GetByStatus(
            byte status)
    {
        var applications =
            await _applicationService
                .GetByStatusAsync(status);

        return Ok(applications);
    }


    [HttpGet("Count")]
    public async Task<IActionResult> GetCount()
    {
        var count =
            await _applicationService
                .CountAsync();

        return Ok(new { count });
    }

    #endregion


    #region POST

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateDvldApplicationRequest request)
    {
        var applicationId =
            await _applicationService
                .CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = applicationId },
            new { applicationId });
    }

    #endregion


    #region PUT

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateDvldApplicationRequest request)
    {
        var updated =
            await _applicationService
                .UpdateAsync(id, request);

        if (!updated)
            return NotFound();

        return NoContent();
    }


    [HttpPut("{id:int}/status")]
    public async Task<IActionResult>
        ChangeStatus(
            int id,
            ChangeApplicationStatusRequest request)
    {
        var updated =
            await _applicationService
                .ChangeStatusAsync(
                    id,
                    request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    #endregion


    #region DELETE

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id)
    {
        var deleted =
            await _applicationService
                .DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

    #endregion
}