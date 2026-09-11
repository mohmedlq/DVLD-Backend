using DVLD.Application.DTOs;
using DVLD.Domain.Entities;
using InfrastructureApplicationType = DVLD.Infrastructure.Entities.ApplicationType;
using InfrastructureDriver = DVLD.Infrastructure.Entities.Driver;
using InfrastructurePerson = DVLD.Infrastructure.Entities.Person;
using InfrastructureUser = DVLD.Infrastructure.Entities.User;

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
            entity.IsActive
            );
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
    public static DetainedLicense MapDetainedLicenseToDomain(
    Infrastructure.Entities.DetainedLicense entity)
    {
        return new DetainedLicense(
            entity.DetainId,
            entity.LicenseId,
            entity.DetainDate,
            entity.FineFees,
            entity.CreatedByUserId,
            entity.IsReleased,
            entity.ReleaseDate,
            entity.ReleasedByUserId,
            entity.ReleaseApplicationId);
    }
    public static Infrastructure.Entities.DetainedLicense
    MapDetainedLicenseToEntity(
        DetainedLicense detainedLicense)
    {
        return new Infrastructure.Entities.DetainedLicense
        {
            DetainId = detainedLicense.DetainId,
            LicenseId = detainedLicense.LicenseId,
            DetainDate = detainedLicense.DetainDate,
            FineFees = detainedLicense.FineFees,
            CreatedByUserId =
                detainedLicense.CreatedByUserId,
            IsReleased =
                detainedLicense.IsReleased,
            ReleaseDate =
                detainedLicense.ReleaseDate,
            ReleasedByUserId =
                detainedLicense.ReleasedByUserId,
            ReleaseApplicationId =
                detainedLicense.ReleaseApplicationId
        };
    }
    public static DvldApplication
    MapDvldApplicationToDomain(
        Infrastructure.Entities.DvldApplication entity)
    {
        return new DvldApplication(
            entity.ApplicationId,
            entity.ApplicantPersonId,
            entity.ApplicationDate,
            entity.ApplicationTypeId,
            entity.ApplicationStatus,
            entity.LastStatusDate,
            entity.PaidFees,
            entity.CreatedByUserId);
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
    public static Infrastructure.Entities.DvldApplication
    MapDvldApplicationToEntity(
        DvldApplication application)
    {
        return new Infrastructure.Entities.DvldApplication
        {
            ApplicationId =
                application.ApplicationId,

            ApplicantPersonId =
                application.ApplicantPersonId,

            ApplicationDate =
                application.ApplicationDate,

            ApplicationTypeId =
                application.ApplicationTypeId,

            ApplicationStatus =
                application.ApplicationStatus,

            LastStatusDate =
                application.LastStatusDate,

            PaidFees =
                application.PaidFees,

            CreatedByUserId =
                application.CreatedByUserId
        };
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
    public static Country MapCountryToDomain(
    Infrastructure.Entities.Country entity)
    {
        return new Country(
            entity.CountryId,
            entity.CountryName);
    }


    public static Infrastructure.Entities.Country MapCountryToEntity(
        Country country)
    {
        return new Infrastructure.Entities.Country
        {
            CountryId = country.CountryId,
            CountryName = country.CountryName
        };
    }
}