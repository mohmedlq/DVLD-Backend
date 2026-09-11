using DVLD.Domain.Entities;

namespace DVLD.Infrastructure.Mappers;

public static class DomainMapper
{
    public static Driver MapToDomain(
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
    public static ApplicationType MapApplicationTypeToDomain(
    Infrastructure.Entities.ApplicationType entity)
    {
        return new ApplicationType(
            entity.ApplicationTypeId,
            entity.ApplicationTypeTitle,
            entity.ApplicationFees);
    }
    public static Infrastructure.Entities.ApplicationType
    MapApplicationTypeToEntity(
        ApplicationType applicationType)
    {
        return new Infrastructure.Entities.ApplicationType
        {
            ApplicationTypeId =
                applicationType.ApplicationTypeId,

            ApplicationTypeTitle =
                applicationType.ApplicationTypeTitle,

            ApplicationFees =
                applicationType.ApplicationFees
        };
    }
    public static User MapUserToDomain(
        Infrastructure.Entities.User entity)
    {
        var person = MapPersonToDomain(entity.Person);

        return new User(
            entity.UserId,
            entity.PersonId,
            entity.UserName,
            entity.Password,
            entity.IsActive
            );
    }
}