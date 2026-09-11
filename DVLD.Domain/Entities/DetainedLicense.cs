namespace DVLD.Domain.Entities;

public class DetainedLicense
{
    #region Properties

    public int DetainId { get; private set; }

    public int LicenseId { get; private set; }

    public DateTime DetainDate { get; private set; }

    public decimal FineFees { get; private set; }

    public int CreatedByUserId { get; private set; }

    public bool IsReleased { get; private set; }

    public DateTime? ReleaseDate { get; private set; }

    public int? ReleasedByUserId { get; private set; }

    public int? ReleaseApplicationId { get; private set; }

    #endregion


    #region Constructors

    // Create
    public DetainedLicense(
        int licenseId,
        decimal fineFees,
        int createdByUserId)
    {
        ValidateLicenseId(licenseId);
        ValidateFineFees(fineFees);
        LicenseId = licenseId;
        FineFees = fineFees;
        CreatedByUserId = createdByUserId;
        DetainDate = DateTime.Now;
        IsReleased = false;
    }


    // Load existing DetainedLicense from database
    public DetainedLicense(
        int detainId,
        int licenseId,
        DateTime detainDate,
        decimal fineFees,
        int createdByUserId,
        bool isReleased,
        DateTime? releaseDate,
        int? releasedByUserId,
        int? releaseApplicationId)
    {
        DetainId = detainId;
        LicenseId = licenseId;
        DetainDate = detainDate;
        FineFees = fineFees;
        CreatedByUserId = createdByUserId;
        IsReleased = isReleased;
        ReleaseDate = releaseDate;
        ReleasedByUserId = releasedByUserId;
        ReleaseApplicationId = releaseApplicationId;
    }

    #endregion


    #region Methods

    public void UpdateInformation(
        decimal fineFees)
    {
        ValidateFineFees(fineFees);

        FineFees = fineFees;
    }


    public void Release(
        int releasedByUserId,
        int? releaseApplicationId)
    {

        if (IsReleased)
            throw new InvalidOperationException(
                "License is already released.");

        IsReleased = true;
        ReleaseDate = DateTime.Now;
        ReleasedByUserId = releasedByUserId;
        ReleaseApplicationId = releaseApplicationId;
    }

    #endregion


    #region Validation

    private static void ValidateLicenseId(int licenseId)
    {
        if (licenseId <= 0)
            throw new ArgumentException(
                "License ID must be greater than 0.");
    }



    private static void ValidateFineFees(decimal fineFees)
    {
        if (fineFees < 0)
            throw new ArgumentException(
                "Fine fees cannot be negative.");
    }

    #endregion
}