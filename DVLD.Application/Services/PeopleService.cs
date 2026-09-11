using DVLD.Application.DTOs;
using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;

namespace DVLD.Application.Services;

public class PeopleService : IPeopleService
{
    private readonly IPeopleRepository _repository;

    public PeopleService(IPeopleRepository repository)
    {
        _repository = repository;
    }

    public async Task<PersonDto?> GetByIdAsync(int personId)
    {
        var person = await _repository.GetByIdAsync(personId);

        if (person == null)
            return null;

        return MapToDto(person);
    }

    public async Task<PersonDto?> GetByNationalNoAsync(
        string nationalNo)
    {
        var person =
            await _repository.GetByNationalNoAsync(nationalNo);

        if (person == null)
            return null;

        return MapToDto(person);
    }

    public async Task<PersonDto?> GetByUsernameAsync(
        string username)
    {
        var person =
            await _repository.GetByUsernameAsync(username);

        if (person == null)
            return null;

        return MapToDto(person);
    }

    public async Task<PersonDto?> LoginAsync(
        string username,
        string password)
    {
        var person =
            await _repository.GetByUsernameAndPasswordAsync(
                username,
                password);

        if (person == null)
            return null;

        return MapToDto(person);
    }

    public async Task<List<PersonDto>> GetAllAsync()
    {
        var people = await _repository.GetAllAsync();

        return people
            .Select(MapToDto)
            .ToList();
    }

    public async Task<int> CreateAsync(
        CreatePersonRequest request)
    {
        // Rules that require checking the database.

        if (await _repository
            .UsernameExistsAsync(request.UserName))
        {
            return -1;
        }

        if (!string.IsNullOrWhiteSpace(request.NationalNo) &&
            await _repository
                .NationalNoExistsAsync(request.NationalNo))
        {
            return -1;
        }

        // Domain handles Person's own rules.

        var person = new Person(
            request.NationalNo!,
            request.FirstName!,
            request.SecondName!,
            request.ThirdName,
            request.LastName!,
            request.DateOfBirth!.Value,
            request.Gender!.Value,
            request.Address!,
            request.Phone!,
            request.Email,
            request.CountryId!.Value,
            request.ImagePath,
            request.UserName,
            request.Password
        );

        await _repository.AddAsync(person);

        return person.PersonId;
    }

    public async Task<int> CreateBasicAsync(
        string username,
        string password)
    {
        return await _repository.AddBasicAsync(
            username,
            password);
    }

    public async Task<bool> UpdateAsync(
        int personId,
        UpdatePersonRequest request)
    {
        var person =
            await _repository.GetByIdAsync(personId);

        if (person == null)
            return false;

        if (!string.IsNullOrWhiteSpace(request.NationalNo) &&
            request.NationalNo != person.NationalNo)
        {
            if (await _repository
                .NationalNoExistsAsync(request.NationalNo))
            {
                return false;
            }
        }

        person.UpdateInformation(
            request.FirstName!,
            request.SecondName!,
            request.ThirdName,
            request.LastName!,
            request.DateOfBirth!.Value,
            request.Gender!.Value,
            request.Address!,
            request.Phone!,
            request.Email,
            request.CountryId!.Value,
            request.ImagePath
        );

        await _repository.UpdateAsync(person);

        return true;
    }

    public async Task<bool> UpdatePasswordAsync(
        int personId,
        string password)
    {
        // Domain validates the password.

        var person =
            await _repository.GetByIdAsync(personId);

        if (person == null)
            return false;

        person.ChangePassword(password);

        await _repository.UpdateAsync(person);

        return true;
    }

    public async Task<bool> ChangePasswordAsync(
        string username,
        string password)
    {
        var person =
            await _repository.GetByUsernameAsync(username);

        if (person == null)
            return false;

        person.ChangePassword(password);

        await _repository.UpdateAsync(person);

        return true;
    }

    public async Task<bool> DeleteAsync(int personId)
    {
        return await _repository.DeleteAsync(personId);
    }

    private static PersonDto MapToDto(Person person)
    {
        return new PersonDto
        {
            PersonId =(int) person.PersonId,
            NationalNo = person.NationalNo,
            FirstName = person.FirstName,
            SecondName = person.SecondName,
            ThirdName = person.ThirdName,
            LastName = person.LastName,
            DateOfBirth = person.DateOfBirth,
            Gender = person.Gender,
            Address = person.Address,
            Phone = person.Phone,
            Email = person.Email,
            CountryId = person.CountryId,
            ImagePath = person.ImagePath,
            UserName = person.UserName
        };
    }
}