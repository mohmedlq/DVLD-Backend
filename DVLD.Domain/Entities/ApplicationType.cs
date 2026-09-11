namespace DVLD.Domain.Entities;

public class ApplicationType
{

    public int ApplicationTypeId { get; private set; }

    public string ApplicationTypeTitle { get; private set; }

    public decimal ApplicationFees { get; private set; }




    // Create
    public ApplicationType(
        string applicationTypeTitle,
        decimal applicationFees)
    {
        ValidateTitle(applicationTypeTitle);
        ValidateFees(applicationFees);

        ApplicationTypeTitle =
            applicationTypeTitle.Trim();

        ApplicationFees = applicationFees;
    }


    // Load existing ApplicationType from database
    public ApplicationType(
        int applicationTypeId,
        string applicationTypeTitle,
        decimal applicationFees)
    {
        ApplicationTypeId = applicationTypeId;
        ApplicationTypeTitle = applicationTypeTitle;
        ApplicationFees = applicationFees;
    }




    public void UpdateInformation(
        string applicationTypeTitle,
        decimal applicationFees)
    {
        ValidateTitle(applicationTypeTitle);
        ValidateFees(applicationFees);

        ApplicationTypeTitle =
            applicationTypeTitle.Trim();

        ApplicationFees = applicationFees;
    }

    



    private static void ValidateTitle(
        string applicationTypeTitle)
    {
        if (string.IsNullOrWhiteSpace(applicationTypeTitle))
            throw new ArgumentException(
                "Application type title is required.");
    }


    private static void ValidateFees(
        decimal applicationFees)
    {
        if (applicationFees < 0)
            throw new ArgumentException(
                "Application fees cannot be negative.");
    }

    
}