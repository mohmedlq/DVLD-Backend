using DVLD.Application.DTOs;
using DVLD.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.API.Controllers;

[ApiController]
[Route("api/Drivers")]
public class DriversController : ControllerBase
{
    private readonly IDriverService _driverService;

    public DriversController(IDriverService driverService)
    {
        _driverService = driverService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var drivers = await _driverService.GetAllAsync();

        return Ok(drivers);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var driver = await _driverService.GetByIdAsync(id);

        if (driver == null)
            return NotFound();

        return Ok(driver);
    }

    [HttpGet("Person/{personId:int}")]
    public async Task<IActionResult> GetByPersonId(int personId)
    {
        var driver = await _driverService.GetByPersonIdAsync(personId);

        if (driver == null)
            return NotFound();

        return Ok(driver);
    }

    [HttpGet("CreatedBy/{userId:int}")]
    public async Task<IActionResult> GetByCreatedByUserId(int userId)
    {
        var drivers = await _driverService.GetByCreatedByUserIdAsync(userId);

        return Ok(drivers);
    }

    [HttpGet("Count")]
    public async Task<IActionResult> GetCount()
    {
        var count = await _driverService.CountAsync();

        return Ok(new { count });
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDriverRequest request)
    {
        var driverId = await _driverService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = driverId },
            new { driverId });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateDriverRequest request)
    {
        var updated = await _driverService.UpdateAsync(id, request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _driverService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}