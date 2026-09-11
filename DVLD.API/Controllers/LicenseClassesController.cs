using DVLD.Application.DTOs;
using DVLD.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LicenseClassesController : ControllerBase
{
    private readonly ILicenseClassService _licenseClassService;

    public LicenseClassesController(ILicenseClassService licenseClassService)
    {
        _licenseClassService = licenseClassService;
    }

    #region Get
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var licenseClasses = await _licenseClassService.GetAllAsync();

        return Ok(licenseClasses);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var licenseClass = await _licenseClassService.GetByIdAsync(id);

        if (licenseClass == null)
            return NotFound();

        return Ok(licenseClass);
    }

    [HttpGet("ByName/{className}")]
    public async Task<IActionResult> GetByName(string className)
    {
        var licenseClass = await _licenseClassService.GetByNameAsync(className);

        if (licenseClass == null)
            return NotFound();

        return Ok(licenseClass);
    }
    #endregion

    #region Create
    [HttpPost]
    public async Task<IActionResult> Create(CreateLicenseClassRequest request)
    {
        var licenseClassId = await _licenseClassService.CreateAsync(request);

        if (licenseClassId == -1)
            return BadRequest("License Class with this name already exists");

        return CreatedAtAction(
            nameof(