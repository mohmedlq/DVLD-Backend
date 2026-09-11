using System.ComponentModel.DataAnnotations;

namespace DVLD.Application.DTOs;

public class CountryDto
{
    public int CountryId { get; set; }

    public string CountryName { get; set; } = null!;
}


public class CreateCountryRequest
{
    [Required]
    public string CountryName { get; set; } = null!;
}


public class UpdateCountryRequest
{
    [Required]
    public string CountryName { get; set; } = null!;
}