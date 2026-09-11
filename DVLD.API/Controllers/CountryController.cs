using DVLD.Application.DTOs;
using DVLD.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.API.Controllers;

[ApiController]
[Route("api/Countries")]
public class CountriesController : ControllerBase
{
    private readonly ICountryService _countryService;

    #region Constructor

    public CountriesController(
        ICountryService countryService)
    {
        _countryService = countryService;
    }

    #endregion


    #region GET

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var countries = await _countryService.GetAllAsync();

        return Ok(countries);
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var country = await _countryService.GetByIdAsync(id);

        if (country == null)
            return NotFound();

        return Ok(country);
    }


    [HttpGet("Count")]
    public async Task<IActionResult> GetCount()
    {
        var count = await _countryService.CountAsync();

        return Ok(new { count });
    }

    #endregion


    #region POST

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateCountryRequest request)
    {
        var countryId =
            await _countryService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = countryId },
            new { countryId });
    }

    #endregion


    #region PUT

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateCountryRequest request)
    {
        var updated =
            await _countryService.UpdateAsync(
                id,
                request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    #endregion


    #region DELETE

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted =
            await _countryService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

    #endregion
}