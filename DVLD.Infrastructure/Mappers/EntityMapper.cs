using DVLD.Domain.Entities;
using InfrastructureUser = DVLD.Infrastructure.Entities.User;
using InfrastructurePerson = DVLD.Infrastructure.Entities.Person;
using InfrastructureDriver = DVLD.Infrastructure.Entities.Driver;
namespace DVLD.Infrastructure.Mappers;

public static class EntityMapper
{
    // Infrastructure → Domain

    public static User MapUserToDomain(
        Infrastructure.Entities.User entity)
    {
        var person = MapPersonToDomain(entity.Person);

        return new User(
            entity.UserId,
            entity.PersonId,
            entity.UserName,
            entity.Password,
            entity.IsActive,
            person);
    }

    public static Person MapPersonToDomain(
        Infrastructure.Entities.Person entity)
    {
        return new Person(
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
    }

    public static Driver MapDriverToDomain(
        Infrastructure.Entities.Driver entity)
    {
        var person = MapPersonToDomain(entity.Person);
        var user = MapUserToDomain(entity.CreatedByUser);

        return new Driver(
            entity.DriverId,
            entity.PersonId,
            entity.CreatedByUserId,
            entity.CreatedDate,
            person,
            user);
    }


    // Domain → Infrastructure

    public static Infrastructure.Entities.Driver MapDriverToEntity(
        Driver driver)
    {
        return new Infrastructure.Entities.Driver
        {
            DriverId = driver.DriverId,
            PersonId = driver.PersonId,
            CreatedByUserId = driver.CreatedByUserId,
            CreatedDate = driver.CreatedDate
        };
    }
}