using System;
using System.Collections.Generic;
using DVLD.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace DVLD.Infrastructure.Data;

public partial class DvldDbContext : DbContext
{
    public DvldDbContext()
    {
    }

    public DvldDbContext(DbContextOptions<DvldDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DvldApplication> Applications { get; set; }

    public virtual DbSet<ApplicationType> ApplicationTypes { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<DeletedLicense> DeletedLicenses { get; set; }

    public virtual DbSet<DetainedLicense> DetainedLicenses { get; set; }

    public virtual DbSet<DetainedLicensesView> DetainedLicensesViews { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<DriversView> DriversViews { get; set; }

    public virtual DbSet<InternationalLicense> InternationalLicenses { get; set; }

    public virtual DbSet<License> Licenses { get; set; }

    public virtual DbSet<LicenseClass> LicenseClasses { get; set; }

    public virtual DbSet<LocalDrivingLicenseApplication> LocalDrivingLicenseApplications { get; set; }

    public virtual DbSet<LocalDrivingLicenseApplicationsView> LocalDrivingLicenseApplicationsViews { get; set; }

    public virtual DbSet<LocalDrivingLicenseFullApplicationsView> LocalDrivingLicenseFullApplicationsViews { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Massage> Massages { get; set; }

    public virtual DbSet<OldUserNamesLog> OldUserNamesLogs { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<DvldTask> Tasks { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<TestAppointment> TestAppointments { get; set; }

    public virtual DbSet<TestAppointmentsView> TestAppointmentsViews { get; set; }

    public virtual DbSet<TestType> TestTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost;Database=DVLD;User Id=sa;Password=123456;TrustServerCertificate=True;");
    }


    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Arabic_CI_AI");


        // =========================================================
        // Applications
        // =========================================================

        modelBuilder.Entity<DvldApplication>(entity =>
        {
            // IMPORTANT:
            // Because the C# class was renamed from Application
            // to DvldApplication, EF can no longer discover
            // ApplicationId by convention.
            entity.HasKey(e => e.ApplicationId);

            entity.Property(e => e.ApplicationId)
                .HasColumnName("ApplicationID");

            entity.Property(e => e.ApplicantPersonId)
                .HasColumnName("ApplicantPersonID");

            entity.Property(e => e.ApplicationDate)
                .HasColumnType("datetime");

            entity.Property(e => e.ApplicationStatus)
                .HasDefaultValue((byte)1)
                .HasComment("1-New 2-Cancelled 3-Completed");

            entity.Property(e => e.ApplicationTypeId)
                .HasColumnName("ApplicationTypeID");

            entity.Property(e => e.CreatedByUserId)
                .HasColumnName("CreatedByUserID");

            entity.Property(e => e.LastStatusDate)
                .HasColumnType("datetime");

            entity.Property(e => e.PaidFees)
                .HasColumnType("smallmoney");


            entity.HasOne(d => d.ApplicantPerson)
                .WithMany(p => p.Applications)
                .HasForeignKey(d => d.ApplicantPersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Applications_People");


            entity.HasOne(d => d.ApplicationType)
                .WithMany(p => p.Applications)
                .HasForeignKey(d => d.ApplicationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Applications_ApplicationTypes");


            entity.HasOne(d => d.CreatedByUser)
                .WithMany(p => p.Applications)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Applications_Users");
        });


        // =========================================================
        // Application Types
        // =========================================================

        modelBuilder.Entity<ApplicationType>(entity =>
        {
            entity.Property(e => e.ApplicationTypeId)
                .HasColumnName("ApplicationTypeID");

            entity.Property(e => e.ApplicationFees)
                .HasColumnType("smallmoney");

            entity.Property(e => e.ApplicationTypeTitle)
                .HasMaxLength(150);
        });


        // =========================================================
        // Countries
        // =========================================================

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId)
                .HasName("PK__Countrie__10D160BFDBD6933F");

            entity.Property(e => e.CountryId)
                .HasColumnName("CountryID");

            entity.Property(e => e.CountryName)
                .HasMaxLength(50);
        });


        // =========================================================
        // Deleted Licenses
        // =========================================================

        modelBuilder.Entity<DeletedLicense>(entity =>
        {
            entity.HasKey(e => e.DeletedId)
                .HasName("PK__Deleted___ABB7422597539328");

            entity.ToTable("Deleted_Licenses");

            entity.Property(e => e.DeletedId)
                .HasColumnName("DeletedID");

            entity.Property(e => e.ApplicationId)
                .HasColumnName("ApplicationID");

            entity.Property(e => e.CreatedByUserId)
                .HasColumnName("CreatedByUserID");

            entity.Property(e => e.DeletionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.Property(e => e.DriverId)
                .HasColumnName("DriverID");

            entity.Property(e => e.ExpirationDate)
                .HasColumnType("datetime");

            entity.Property(e => e.IssueDate)
                .HasColumnType("datetime");

            entity.Property(e => e.LicenseId)
                .HasColumnName("LicenseID");

            entity.Property(e => e.Notes)
                .HasMaxLength(500);

            entity.Property(e => e.PaidFees)
                .HasColumnType("smallmoney");
        });


        // =========================================================
        // Detained Licenses
        // =========================================================

        modelBuilder.Entity<DetainedLicense>(entity =>
        {
            entity.HasKey(e => e.DetainId);

            entity.Property(e => e.DetainId)
                .HasColumnName("DetainID");

            entity.Property(e => e.CreatedByUserId)
                .HasColumnName("CreatedByUserID");

            entity.Property(e => e.DetainDate)
                .HasColumnType("smalldatetime");

            entity.Property(e => e.FineFees)
                .HasColumnType("smallmoney");

            entity.Property(e => e.LicenseId)
                .HasColumnName("LicenseID");

            entity.Property(e => e.ReleaseApplicationId)
                .HasColumnName("ReleaseApplicationID");

            entity.Property(e => e.ReleaseDate)
                .HasColumnType("smalldatetime");

            entity.Property(e => e.ReleasedByUserId)
                .HasColumnName("ReleasedByUserID");


            entity.HasOne(d => d.CreatedByUser)
                .WithMany(p => p.DetainedLicenseCreatedByUsers)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetainedLicenses_Users");


            entity.HasOne(d => d.License)
                .WithMany(p => p.DetainedLicenses)
                .HasForeignKey(d => d.LicenseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetainedLicenses_Licenses");


            entity.HasOne(d => d.ReleaseApplication)
                .WithMany(p => p.DetainedLicenses)
                .HasForeignKey(d => d.ReleaseApplicationId)
                .HasConstraintName("FK_DetainedLicenses_Applications");


            entity.HasOne(d => d.ReleasedByUser)
                .WithMany(p => p.DetainedLicenseReleasedByUsers)
                .HasForeignKey(d => d.ReleasedByUserId)
                .HasConstraintName("FK_DetainedLicenses_Users1");
        });


        // =========================================================
        // Detained Licenses View
        // =========================================================

        modelBuilder.Entity<DetainedLicensesView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("DetainedLicenses_View");

            entity.Property(e => e.DetainDate)
                .HasColumnType("smalldatetime");

            entity.Property(e => e.DetainId)
                .HasColumnName("DetainID");

            entity.Property(e => e.FineFees)
                .HasColumnType("smallmoney");

            entity.Property(e => e.FullName)
                .HasMaxLength(83);

            entity.Property(e => e.LicenseId)
                .HasColumnName("LicenseID");

            entity.Property(e => e.NationalNo)
                .HasMaxLength(20);

            entity.Property(e => e.ReleaseApplicationId)
                .HasColumnName("ReleaseApplicationID");

            entity.Property(e => e.ReleaseDate)
                .HasColumnName("ReleaseDate")
                .HasColumnType("smalldatetime");
        });


        // =========================================================
        // Drivers
        // =========================================================

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.DriverId)
                .HasName("PK_Drivers_1");

            entity.Property(e => e.DriverId)
                .HasColumnName("DriverID");

            entity.Property(e => e.CreatedByUserId)
                .HasColumnName("CreatedByUserID");

            entity.Property(e => e.CreatedDate)
                .HasColumnType("smalldatetime");

            entity.Property(e => e.PersonId)
                .HasColumnName("PersonID");


            entity.HasOne(d => d.CreatedByUser)
                .WithMany(p => p.Drivers)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Drivers_Users");


            entity.HasOne(d => d.Person)
                .WithMany(p => p.Drivers)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Drivers_People");
        });


        // =========================================================
        // Drivers View
        // =========================================================

        modelBuilder.Entity<DriversView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Drivers_View");

            entity.Property(e => e.CreatedDate)
                .HasColumnType("smalldatetime");

            entity.Property(e => e.DriverId)
                .HasColumnName("DriverID");

            entity.Property(e => e.FullName)
                .HasMaxLength(83);

            entity.Property(e => e.NationalNo)
                .HasMaxLength(20);

            entity.Property(e => e.PersonId)
                .HasColumnName("PersonID");
        });


        // =========================================================
        // International Licenses
        // =========================================================

        modelBuilder.Entity<InternationalLicense>(entity =>
        {
            entity.Property(e => e.InternationalLicenseId)
                .HasColumnName("InternationalLicenseID");

            entity.Property(e => e.ApplicationId)
                .HasColumnName("ApplicationID");

            entity.Property(e => e.CreatedByUserId)
                .HasColumnName("CreatedByUserID");

            entity.Property(e => e.DriverId)
                .HasColumnName("DriverID");

            entity.Property(e => e.ExpirationDate)
                .HasColumnType("smalldatetime");

            entity.Property(e => e.IssueDate)
                .HasColumnType("smalldatetime");

            entity.Property(e => e.IssuedUsingLocalLicenseId)
                .HasColumnName("IssuedUsingLocalLicenseID");


            entity.HasOne(d => d.Application)
                .WithMany(p => p.InternationalLicenses)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InternationalLicenses_Applications");


            entity.HasOne(d => d.CreatedByUser)
                .WithMany(p => p.InternationalLicenses)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InternationalLicenses_Users");


            entity.HasOne(d => d.Driver)
                .WithMany(p => p.InternationalLicenses)
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InternationalLicenses_Drivers");


            entity.HasOne(d => d.IssuedUsingLocalLicense)
                .WithMany(p => p.InternationalLicenses)
                .HasForeignKey(d => d.IssuedUsingLocalLicenseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InternationalLicenses_Licenses");
        });


        // =========================================================
        // Licenses
        // =========================================================

        modelBuilder.Entity<License>(entity =>
        {
            entity.ToTable(tb =>
                tb.HasTrigger("trg_ArchiveDeletedLicenses"));

            entity.Property(e => e.LicenseId)
                .HasColumnName("LicenseID");

            entity.Property(e => e.ApplicationId)
                .HasColumnName("ApplicationID");

            entity.Property(e => e.CreatedByUserId)
                .HasColumnName("CreatedByUserID");

            entity.Property(e => e.DriverId)
                .HasColumnName("DriverID");

            entity.Property(e => e.ExpirationDate)
                .HasColumnType("datetime");

            entity.Property(e => e.IsActive)
                .HasDefaultValue(true);

            entity.Property(e => e.IssueDate)
                .HasColumnType("datetime");

            entity.Property(e => e.IssueReason)
                .HasDefaultValue((byte)1)
                .HasComment(
                    "1-FirstTime, 2-Renew, 3-Replacement for Damaged, 4- Replacement for Lost.");

            entity.Property(e => e.Notes)
                .HasMaxLength(500);

            entity.Property(e => e.PaidFees)
                .HasColumnType("smallmoney");


            entity.HasOne(d => d.Application)
                .WithMany(p => p.Licenses)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Licenses_Applications");


            entity.HasOne(d => d.CreatedByUser)
                .WithMany(p => p.Licenses)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Licenses_Users");


            entity.HasOne(d => d.Driver)
                .WithMany(p => p.Licenses)
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Licenses_Drivers");


            entity.HasOne(d => d.LicenseClassNavigation)
                .WithMany(p => p.Licenses)
                .HasForeignKey(d => d.LicenseClass)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Licenses_LicenseClasses");
        });


        // =========================================================
        // License Classes
        // =========================================================

        modelBuilder.Entity<LicenseClass>(entity =>
        {
            entity.Property(e => e.LicenseClassId)
                .HasColumnName("LicenseClassID");

            entity.Property(e => e.ClassDescription)
                .HasMaxLength(500);

            entity.Property(e => e.ClassFees)
                .HasColumnType("smallmoney");

            entity.Property(e => e.ClassName)
                .HasMaxLength(50);

            entity.Property(e => e.DefaultValidityLength)
                .HasDefaultValue((byte)1)
                .HasComment(
                    "How many years the licesnse will be valid.");

            entity.Property(e => e.MinimumAllowedAge)
                .HasDefaultValue((byte)18)
                .HasComment(
                    "Minmum age allowed to apply for this license");
        });


        // =========================================================
        // Local Driving License Applications
        // =========================================================

        modelBuilder.Entity<LocalDrivingLicenseApplication>(entity =>
        {
            entity.HasKey(e => e.LocalDrivingLicenseApplicationId)
                .HasName("PK_DrivingLicsenseApplications");

            entity.Property(e => e.LocalDrivingLicenseApplicationId)
                .HasColumnName("LocalDrivingLicenseApplicationID");

            entity.Property(e => e.ApplicationId)
                .HasColumnName("ApplicationID");

            entity.Property(e => e.LicenseClassId)
                .HasColumnName("LicenseClassID");


            entity.HasOne(d => d.Application)
                .WithMany(p => p.LocalDrivingLicenseApplications)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName(
                    "FK_DrivingLicsenseApplications_Applications");


            entity.HasOne(d => d.LicenseClass)
                .WithMany(p => p.LocalDrivingLicenseApplications)
                .HasForeignKey(d => d.LicenseClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName(
                    "FK_DrivingLicsenseApplications_LicenseClasses");
        });


        // =========================================================
        // Local Driving License Applications View
        // =========================================================

        modelBuilder.Entity<LocalDrivingLicenseApplicationsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("LocalDrivingLicenseApplications_View");

            entity.Property(e => e.ApplicationDate)
                .HasColumnType("datetime");

            entity.Property(e => e.ClassName)
                .HasMaxLength(50);

            entity.Property(e => e.FullName)
                .HasMaxLength(83);

            entity.Property(e => e.LocalDrivingLicenseApplicationId)
                .HasColumnName("LocalDrivingLicenseApplicationID");

            entity.Property(e => e.NationalNo)
                .HasMaxLength(20);

            entity.Property(e => e.Status)
                .HasMaxLength(9)
                .IsUnicode(false);
        });


        // =========================================================
        // Local Driving License Full Applications View
        // =========================================================

        modelBuilder.Entity<LocalDrivingLicenseFullApplicationsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("LocalDrivingLicenseFullApplications_View");

            entity.Property(e => e.ApplicantPersonId)
                .HasColumnName("ApplicantPersonID");

            entity.Property(e => e.ApplicationDate)
                .HasColumnType("datetime");

            entity.Property(e => e.ApplicationId)
                .HasColumnName("ApplicationID");

            entity.Property(e => e.ApplicationTypeId)
                .HasColumnName("ApplicationTypeID");

            entity.Property(e => e.CreatedByUserId)
                .HasColumnName("CreatedByUserID");

            entity.Property(e => e.LastStatusDate)
                .HasColumnType("datetime");

            entity.Property(e => e.LicenseClassId)
                .HasColumnName("LicenseClassID");

            entity.Property(e => e.LocalDrivingLicenseApplicationId)
                .HasColumnName("LocalDrivingLicenseApplicationID");

            entity.Property(e => e.PaidFees)
                .HasColumnType("smallmoney");
        });


        // =========================================================
        // Logs
        // =========================================================

        modelBuilder.Entity<Log>(entity =>
        {
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.ExceptionType)
                .HasMaxLength(255);

            entity.Property(e => e.HttpMethod)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.Level)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Message)
                .HasMaxLength(1000);

            entity.Property(e => e.RequestPath)
                .HasMaxLength(500);
        });


        // =========================================================
        // Messages
        // =========================================================

        modelBuilder.Entity<Massage>(entity =>
        {
            entity.HasKey(e => e.MessageId)
                .HasName("PK__Massage__C87C037CB528BBA3");

            entity.ToTable("Massage");

            entity.Property(e => e.MessageId)
                .HasColumnName("MessageID");

            entity.Property(e => e.MessageContent)
                .HasMaxLength(3000);

            entity.Property(e => e.MessageTitle)
                .HasMaxLength(250);

            entity.Property(e => e.PersonId)
                .HasColumnName("PersonID");


            entity.HasOne(d => d.Person)
                .WithMany(p => p.Massages)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName(
                    "FK__Massage__PersonI__31A25463");
        });


        // =========================================================
        // Old User Names Log
        // =========================================================

        modelBuilder.Entity<OldUserNamesLog>(entity =>
        {
            entity.HasKey(e => e.LogId)
                .HasName("PK__OldUserN__5E5499A8D4F3ED41");

            entity.ToTable("OldUserNamesLog");

            entity.Property(e => e.LogId)
                .HasColumnName("LogID");

            entity.Property(e => e.NewUserName)
                .HasMaxLength(50);

            entity.Property(e => e.OldUserName)
                .HasMaxLength(50);
        });


        // =========================================================
        // People
        // =========================================================

        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable(tb =>
                tb.HasTrigger("trg_OnUserNameUpdated"));

            entity.Property(e => e.PersonId)
                .HasColumnName("PersonID");

            entity.Property(e => e.Address)
                .HasMaxLength(500);

            entity.Property(e => e.CountryId)
                .HasColumnName("CountryID");

            entity.Property(e => e.DateOfBirth)
                .HasColumnType("datetime");

            entity.Property(e => e.Email)
                .HasMaxLength(100);

            entity.Property(e => e.FirstName)
                .HasMaxLength(20);

            entity.Property(e => e.Gender)
                .HasDefaultValue((byte)0)
                .HasComment("0 Male , 1 Femail");

            entity.Property(e => e.ImagePath)
                .HasMaxLength(255);

            entity.Property(e => e.LastName)
                .HasMaxLength(20);

            entity.Property(e => e.NationalNo)
                .HasMaxLength(20);

            entity.Property(e => e.Password)
                .HasMaxLength(120);

            entity.Property(e => e.Phone)
                .HasMaxLength(20);

            entity.Property(e => e.SecondName)
                .HasMaxLength(20);

            entity.Property(e => e.ThirdName)
                .HasMaxLength(20);

            entity.Property(e => e.UserName)
                .HasMaxLength(64)
                .IsUnicode(false);
        });


        // =========================================================
        // Tasks
        // =========================================================

        modelBuilder.Entity<DvldTask>(entity =>
        {
            entity.HasKey(e => e.TaskId)
                .HasName("PK__Tasks__7C6949D13575F7B8");

            entity.Property(e => e.TaskId)
                .HasColumnName("TaskID");

            entity.Property(e => e.Description)
                .HasMaxLength(1000);

            entity.Property(e => e.TaskTitle)
                .HasMaxLength(40);

            entity.Property(e => e.UserId)
                .HasColumnName("UserID");


            entity.HasOne(d => d.User)
                .WithMany(p => p.Tasks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName(
                    "FK__Tasks__UserID__668030F6");
        });


        // =========================================================
        // Tests
        // =========================================================

        modelBuilder.Entity<Test>(entity =>
        {
            entity.Property(e => e.TestId)
                .HasColumnName("TestID");

            entity.Property(e => e.CreatedByUserId)
                .HasColumnName("CreatedByUserID");

            entity.Property(e => e.Notes)
                .HasMaxLength(500);

            entity.Property(e => e.TestAppointmentId)
                .HasColumnName("TestAppointmentID");

            entity.Property(e => e.TestResult)
                .HasComment("0 - Fail 1-Pass");


            entity.HasOne(d => d.CreatedByUser)
                .WithMany(p => p.Tests)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tests_Users");


            entity.HasOne(d => d.TestAppointment)
                .WithMany(p => p.Tests)
                .HasForeignKey(d => d.TestAppointmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName(
                    "FK_Tests_TestAppointments");
        });


        // =========================================================
        // Test Appointments
        // =========================================================

        modelBuilder.Entity<TestAppointment>(entity =>
        {
            entity.Property(e => e.TestAppointmentId)
                .HasColumnName("TestAppointmentID");

            entity.Property(e => e.AppointmentDate)
                .HasColumnType("smalldatetime");

            entity.Property(e => e.CreatedByUserId)
                .HasColumnName("CreatedByUserID");

            entity.Property(e => e.LocalDrivingLicenseApplicationId)
                .HasColumnName("LocalDrivingLicenseApplicationID");

            entity.Property(e => e.PaidFees)
                .HasColumnType("smallmoney");

            entity.Property(e => e.RetakeTestApplicationId)
                .HasColumnName("RetakeTestApplicationID");

            entity.Property(e => e.TestTypeId)
                .HasColumnName("TestTypeID");


            entity.HasOne(d => d.CreatedByUser)
                .WithMany(p => p.TestAppointments)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName(
                    "FK_TestAppointments_Users");


            entity.HasOne(d => d.LocalDrivingLicenseApplication)
                .WithMany(p => p.TestAppointments)
                .HasForeignKey(d => d.LocalDrivingLicenseApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName(
                    "FK_TestAppointments_LocalDrivingLicenseApplications");


            entity.HasOne(d => d.RetakeTestApplication)
                .WithMany(p => p.TestAppointments)
                .HasForeignKey(d => d.RetakeTestApplicationId)
                .HasConstraintName(
                    "FK_TestAppointments_Applications");


            entity.HasOne(d => d.TestType)
                .WithMany(p => p.TestAppointments)
                .HasForeignKey(d => d.TestTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName(
                    "FK_TestAppointments_TestTypes");
        });


        // =========================================================
        // Test Appointments View
        // =========================================================

        modelBuilder.Entity<TestAppointmentsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TestAppointments_View");

            entity.Property(e => e.AppointmentDate)
                .HasColumnType("smalldatetime");

            entity.Property(e => e.ClassName)
                .HasMaxLength(50);

            entity.Property(e => e.FullName)
                .HasMaxLength(83);

            entity.Property(e => e.LocalDrivingLicenseApplicationId)
                .HasColumnName("LocalDrivingLicenseApplicationID");

            entity.Property(e => e.PaidFees)
                .HasColumnType("smallmoney");

            entity.Property(e => e.TestAppointmentId)
                .HasColumnName("TestAppointmentID");

            entity.Property(e => e.TestTypeTitle)
                .HasMaxLength(100);
        });


        // =========================================================
        // Test Types
        // =========================================================

        modelBuilder.Entity<TestType>(entity =>
        {
            entity.Property(e => e.TestTypeId)
                .HasColumnName("TestTypeID");

            entity.Property(e => e.TestTypeDescription)
                .HasMaxLength(500);

            entity.Property(e => e.TestTypeFees)
                .HasColumnType("smallmoney");

            entity.Property(e => e.TestTypeTitle)
                .HasMaxLength(100);
        });


        // =========================================================
        // Users
        // =========================================================

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable(tb =>
                tb.HasTrigger("trg_InsteadOfDeleteStudent"));

            entity.Property(e => e.UserId)
                .HasColumnName("UserID");

            entity.Property(e => e.Password)
                .HasMaxLength(70);

            entity.Property(e => e.PersonId)
                .HasColumnName("PersonID");

            entity.Property(e => e.UserName)
                .HasMaxLength(20);


            entity.HasOne(d => d.Person)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_People");
        });


        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(
        ModelBuilder modelBuilder);
}