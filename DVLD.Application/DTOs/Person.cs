namespace DVLD.Application.DTOs;

public class PersonDto
{
    public int PersonId { get; set; }

    public string? NationalNo { get; set; }

    public string? FirstName { get; set; }

    public string? SecondName { get; set; }

    public string? ThirdName { get; set; }

    public string? LastName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public byte? Gender { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public int? CountryId { get; set; }

    public string? ImagePath { get; set; }

    public string UserName { get; set; } = string.Empty;
}
public class CreatePersonRequest
{
    public int PersonId { get; set; }
    public string? NationalNo { get; set; }

    public string? FirstName { get; set; }

    public string? SecondName { get; set; }

    public string? ThirdName { get; set; }

    public string? LastName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public byte? Gender { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public int? CountryId { get; set; }

    public string? ImagePath { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
public class UpdatePersonRequest
{
    public string? NationalNo { get; set; }

    public string? FirstName { get; set; }

    public string? SecondName { get; set; }

    public string? ThirdName { get; set; }

    public string? LastName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public byte? Gender { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public int? CountryId { get; set; }

    public string? ImagePath { get; set; }

    public string UserName { get; set; } = string.Empty;
}