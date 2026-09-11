using DVLD.Application.DTOs;
using DVLD.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.API.Controllers;

[ApiController]
[Route("api/People")]
public class PeopleController : ControllerBase
{
    private readonly IPeopleService _peopleService;

    public PeopleController(IPeopleService peopleService)
    {
        _peopleService = peopleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var people = await _peopleService.GetAllAsync();

        return Ok(people);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var person = await _peopleService.GetByIdAsync(id);

        if (person == null)
            return NotFound();

        return Ok(person);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreatePersonRequest request)
    {
        var personId = await _peopleService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = personId },
            new { personId });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdatePersonRequest request)
    {
        var updated = await _peopleService.UpdateAsync(
            id,
            request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpPut("{id:int}/password")]
    public async Task<IActionResult> UpdatePassword(ChangePasswordRequest request)
    {
        var updated = await _peopleService.ChangePasswordAsync(
            request.UserName,
            request.NewPassword);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _peopleService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}