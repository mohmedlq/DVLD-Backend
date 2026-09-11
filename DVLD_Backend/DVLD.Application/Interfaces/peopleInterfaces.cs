using DVLD.Application.DTOs;
using DVLD.Domain.Entities;
using System;

namespace DVLD.Application.Interfaces;

public interface IPeopleRepository:ICrud<Person>
{

    Task<Person?> GetByNationalNoAsync(string nationalNo);

    Task<Person?> GetByUsernameAsync(string username);

    Task<Person?> GetByUsernameAndPasswordAsync(
        string username,
        string password);


    Task<bool> ExistsAsync(int personId);

    Task<bool> NationalNoExistsAsync(string nationalNo);

    Task<bool> UsernameExistsAsync(string username);


    Task<int> AddBasicAsync(
        string username,
        string password);


    Task<bool> UpdatePasswordAsync(
        int personId,
        string password);

    Task<bool> ChangePasswordAsync(
        string username,
        string password);

}

public interface IPeopleService
{
    Task<PersonDto?> GetByIdAsync(int personId);

    Task<PersonDto?> GetByNationalNoAsync(string nationalNo);

    Task<PersonDto?> GetByUsernameAsync(string username);

    Task<PersonDto?> LoginAsync(
        string username,
        string password);

    Task<List<PersonDto>> GetAllAsync();

    Task<int> CreateAsync(
        CreatePersonRequest request);

    Task<int> CreateBasicAsync(
        string username,
        string password);

    Task<bool> UpdateAsync(
        int personId,
        UpdatePersonRequest request);

    Task<bool> UpdatePasswordAsync(
        int personId,
        string password);

    Task<bool> ChangePasswordAsync(
        string username,
        string password);

    Task<bool> DeleteAsync(int personId);
}
