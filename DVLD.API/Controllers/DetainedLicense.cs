using DVLD.Application.DTOs;
using DVLD.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.API.Controllers;

[ApiController]
[Route("api/DetainedLicenses")]
public class DetainedLicensesController
    : ControllerBase
{
    #region Fields

    private readonly IDetainedLicenseService
        _detainedLicenseService;

    #endregion


    #region Constructor

    public DetainedLicensesController(
        IDetainedLicenseService detainedLicenseService)
    {
        _detainedLicenseService =
            detainedLicenseService;
    }

    #endregion


    #region GET

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var detainedLicenses =
            await _detainedLicenseService
                .GetAllAsync();

        return Ok(detainedLicenses);
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var detainedLicense =
            await _detainedLicenseService
                .GetByIdAsync(id);

        if (detainedLicense == null)
            return NotFound();

        return Ok(detainedLicense);
    }


    [HttpGet("License/{licenseId:int}")]
    public async Task<IActionResult> GetByLicenseId(
        int licenseId)
    {
        var detainedLicenses =
            await _detainedLicenseService
                .GetByLicenseIdAsync(licenseId);

        return Ok(detainedLicenses);
    }


    [HttpGet("CreatedBy/{userId:int}")]
    public async Task<IActionResult>
        GetByCreatedByUserId(int userId)
    {
        var detainedLicenses =
            await _detainedLicenseService
                .GetByCreatedByUserIdAsync(userId);

        return Ok(detainedLicenses);
    }


    [HttpGet("Released")]
    public async Task<IActionResult> GetReleased()
    {
        var detainedLicenses =
            await _detainedLicenseService
                .GetReleasedAsync();

        return Ok(detainedLicenses);
    }


    [HttpGet("Unreleased")]
    public async Task<IActionResult> GetUnreleased()
    {
        var detainedLicenses =
            await _detainedLicenseService
                .GetUnreleasedAsync();

        return Ok(detainedLicenses);
    }


    [HttpGet("Count")]
    public async Task<IActionResult> GetCount()
    {
        var count =
            await _detainedLicenseService
                .CountAsync();

        return Ok(new { count });
    }

    #endregion


    #region POST

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateDetainedLicenseRequest request)
    {
        var detainId =
            await _detainedLicenseService
                .CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = detainId },
            new { detainId });
    }

    #endregion


    #region PUT

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateDetainedLicenseRequest request)
    {
        var updated =
            await _detainedLicenseService
                .UpdateAsync(id, request);

        if (!updated)
            return NotFound();

        return NoContent();
    }


    [HttpPut("{id:int}/release")]
    public async Task<IActionResult> Release(
        int id,
        ReleaseDetainedLicenseRequest request)
    {
        var released =
            await _detainedLicenseService
                .ReleaseAsync(id, request);

        if (!released)
            return NotFound();

        return NoContent();
    }

    #endregion


    #region DELETE

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted =
            await _detainedLicenseService
                .DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

    #endregion
}