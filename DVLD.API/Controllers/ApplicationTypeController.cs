using DVLD.Application.DTOs;
using DVLD.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.API.Controllers;

[ApiController]
[Route("api/ApplicationTypes")]
public class ApplicationTypesController
    : ControllerBase
{
    #region Fields

    private readonly IApplicationTypeService
        _applicationTypeService;

    #endregion


    #region Constructor

    public ApplicationTypesController(
        IApplicationTypeService applicationTypeService)
    {
        _applicationTypeService =
            applicationTypeService;
    }

    #endregion


    #region GET

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var applicationTypes =
            await _applicationTypeService
                .GetAllAsync();

        return Ok(applicationTypes);
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int ApplicationTypeid)
    {
        var applicationType = await _applicationTypeService.GetByIdAsync(ApplicationTypeid);

        if (applicationType == null)
            return NotFound();

        return Ok(applicationType);
    }

    [HttpGet("{id:int}/fees")]
    public async Task<IActionResult> GetFees(int id)
    {
        var applicationType = await _applicationTypeService.GetByIdAsync(id);

        if (applicationType == null)
            return NotFound();

        return Ok(applicationType.ApplicationFees);
    }

    [HttpGet("Count")]
    public async Task<IActionResult> GetCount()
    {
        var count =
            await _applicationTypeService
                .CountAsync();

        return Ok(new { count });
    }

    #endregion


    #region POST

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateApplicationTypeRequest request)
    {
        var applicationTypeId =
            await _applicationTypeService
                .CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = applicationTypeId },
            new { applicationTypeId });
    }

    #endregion


    #region PUT

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateApplicationTypeRequest request)
    {
        var updated =
            await _applicationTypeService
                .UpdateAsync(id, request);

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
            await _applicationTypeService
                .DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

    #endregion
}