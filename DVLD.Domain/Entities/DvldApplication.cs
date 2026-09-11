namespace DVLD.Domain.Entities;

public class DvldApplication
{
    #region Properties

    public int ApplicationId { get; private set; }

    public int ApplicantPersonId { get; private set; }

    public DateTime ApplicationDate { get; private set; }

    public int ApplicationTypeId { get; private set; }

    public byte ApplicationStatus { get; private set; }

    public DateTime LastStatusDate { get; private set; }

    public decimal PaidFees { get; private set; }

    public int CreatedByUserId { get; private set; }

    #endregion


    #region Constructors

    // Create
    public DvldApplication(
        int applicantPersonId,
        int applicationTypeId,
        byte applicationStatus,
        decimal paidFees,
        int createdByUserId)
    {
        ValidatePersonId(applicantPersonId);
        ValidateApplicationTypeId(applicationTypeId);
        ValidateStatus(applicationStatus);
        ValidatePaidFees(paidFees);

        ApplicantPersonId = applicantPersonId;
        ApplicationTypeId = applicationTypeId;
        ApplicationStatus = applicationStatus;
        PaidFees = paidFees;
        CreatedByUserId = createdByUserId;

        ApplicationDate = DateTime.Now;
        LastStatusDate = DateTime.Now;
    }


    // Load existing application from database
    public DvldApplication(
        int applicationId,
        int applicantPersonId,
        DateTime applicationDate,
        int applicationTypeId,
        byte applicationStatus,
        DateTime lastStatusDate,
        decimal paidFees,
        int createdByUserId)
    {
        ApplicationId = applicationId;
        ApplicantPersonId = applicantPersonId;
        ApplicationDate = applicationDate;
        ApplicationTypeId = applicationTypeId;
        ApplicationStatus = applicationStatus;
        LastStatusDate = lastStatusDate;
        PaidFees = paidFees;
        CreatedByUserId = createdByUserId;
    }

    #endregion


    #region Methods

    public void UpdateInformation(
        int applicationTypeId,
        decimal paidFees)
    {
        ValidateApplicationTypeId(applicationTypeId);
        ValidatePaidFees(paidFees);

        ApplicationTypeId = applicationTypeId;
        PaidFees = paidFees;
    }


    public void ChangeStatus(
        byte applicationStatus)
    {
        ValidateStatus(applicationStatus);

        ApplicationStatus = applicationStatus;
        LastStatusDate = DateTime.Now;
    }

    #endregion


    #region Validation

    private static void ValidatePersonId(
        int applicantPersonId)
    {
        if (applicantPersonId <= 0)
            throw new ArgumentException(
                "Applicant person ID must be greater than 0.");
    }


    private static void ValidateApplicationTypeId(
        int applicationTypeId)
    {
        if (applicationTypeId <= 0)
            throw new ArgumentException(
                "Application type ID must be greater than 0.");
    }


   
    private static void ValidateStatus(
        byte applicationStatus)
    {
        if (applicationStatus < 1 ||
            applicationStatus > 3)
        {
            throw new ArgumentException(
                "Application status must be 1, 2, or 3.");
        }
    }


    private static void ValidatePaidFees(
        decimal paidFees)
    {
        if (paidFees < 0)
            throw new ArgumentException(
                "Paid fees cannot be negative.");
    }

    #endregion
}