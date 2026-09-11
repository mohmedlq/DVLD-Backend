using DVLD.Application.Interfaces;
using DVLD.Domain.Entities;
using DVLD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DVLD.Infrastructure.Repositories;

public class PeopleRepository : IPeopleRepository
{
    private readonly DvldDbContext _context;

    public PeopleRepository(DvldDbContext context)
    {
        _context = context;
    }

    public async Task<Person?> GetByIdAsync(int personId)
    {
        var entity = await _context.People
            .AsNoTracking()
            .FirstOrDefaultAsync(p =>
                p.PersonId == personId);

        return entity == null
            ? null
            : MapToDomain(entity);
    }

    public async Task<Person?> GetByNationalNoAsync(
        string nationalNo)
    {
        var entity = await _context.People
            .AsNoTracking()
            .FirstOrDefaultAsync(p =>
                p.NationalNo == nationalNo);

        return entity == null
            ? null
            : MapToDomain(entity);
    }

    public async Task<Person?> GetByUsernameAsync(
        string username)
    {
        var entity = await _context.People
            .AsNoTracking()
            .FirstOrDefaultAsync(p =>
                p.UserName == username);

        return entity == null
            ? null
            : MapToDomain(entity);
    }

    public async Task<Person?> GetByUsernameAndPasswordAsync(
        string username,
        string password)
    {
        var entity = await _context.People
            .AsNoTracking()
            .FirstOrDefaultAsync(p =>
                p.UserName == username &&
                p.Password == password);

        return entity == null
            ? null
            : MapToDomain(entity);
    }

    public async Task<List<Person>> GetAllAsync()
    {
        var entities = await _context.People
            .AsNoTracking()
            .ToListAsync();

        return entities
            .Select(MapToDomain)
            .ToList();
    }

    public async Task<bool> ExistsAsync(int personId)
    {
        return await _context.People.AsNoTracking()
            .AnyAsync(p =>
                p.PersonId == personId);
    }

    public async Task<bool> NationalNoExistsAsync(
        string nationalNo)
    {
        return await _context.People.AsNoTracking()
            .AnyAsync(p =>
                p.NationalNo == nationalNo);
    }

    public async Task<bool> UsernameExistsAsync(
        string username)
    {
        return await _context.People.AsNoTracking()
            .AnyAsync(p =>
                p.UserName == username);
    }

    public async Task AddAsync(Person person)
    {
        var entity = MapToEntity(person);

        await _context.People.AddAsync(entity);

        await _context.SaveChangesAsync();
    }

    public async Task<int> AddBasicAsync(
        string username,
        string password)
    {
        var entity = new Infrastructure.Entities.Person
        {
            UserName = username,
            Password = password
        };

        await _context.People.AddAsync(entity);

        await _context.SaveChangesAsync();

        return entity.PersonId;
    }

    public async Task<bool> UpdateAsync(Person person)
    {
        var entity = await _context.People
            .FirstOrDefaultAsync(p =>
                p.PersonId == person.PersonId);

        if (entity == null)
            throw new KeyNotFoundException(
                $"Person with ID {person.PersonId} was not found.");

        entity.NationalNo = person.NationalNo;
        entity.FirstName = person.FirstName;
        entity.SecondName = person.SecondName;
        entity.ThirdName = person.ThirdName;
        entity.LastName = person.LastName;
        entity.DateOfBirth = person.DateOfBirth;
        entity.Gender = person.Gender;
        entity.Address = person.Address;
        entity.Phone = person.Phone;
        entity.Email = person.Email;
        entity.CountryId = person.CountryId;
        entity.ImagePath = person.ImagePath;
        entity.UserName = person.UserName;
        entity.Password = person.Password;

       return await _context.SaveChangesAsync()>0;
    }

    public async Task<bool> UpdatePasswordAsync(
        int personId,
        string password)
    {
        var entity = await _context.People
            .FirstOrDefaultAsync(p =>
                p.PersonId == personId);

        if (entity == null)
            return false;

        entity.Password = password;

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> ChangePasswordAsync(
        string username,
        string password)
    {
        var entity = await _context.People
            .FirstOrDefaultAsync(p =>
                p.UserName == username);

        if (entity == null)
            return false;

        entity.Password = password;

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int personId)
    {
        var entity = await _context.People
            .FirstOrDefaultAsync(p =>
                p.PersonId == personId);

        if (entity == null)
            return false;

        _context.People.Remove(entity);

        return await _context.SaveChangesAsync() > 0;
    }

    private static Person MapToDomain(
        Infrastructure.Entities.Person entity)
    {
        var person = new Person(
            entity.PersonId,
            entity.NationalNo,
            entity.FirstName,
            entity.SecondName,
            entity.ThirdName,
            entity.LastName,
            entity.DateOfBirth,
            entity.Gender,
            entity.Address,
            entity.Phone,
            entity.Email,
            entity.CountryId,
            entity.ImagePath,
            entity.UserName,
            entity.Password);

        // PersonId has private set.
        // We need a way to assign the database-generated ID.
        // Add a suitable method to the Domain Person later.

        return person;
    }

    private static Infrastructure.Entities.Person MapToEntity(
        Person person)
    {
        return new Infrastructure.Entities.Person
        {
            PersonId = (int)person.PersonId,
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
            UserName = person.UserName,
            Password = person.Password
        };
    }
}