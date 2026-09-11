using System.ComponentModel.DataAnnotations;

namespace DVLD.Application.DTOs;

public class DvldApplicationDto
{
    #region Properties

    public int ApplicationId { get; set; }

    public int ApplicantPersonId { get; set; }

    public DateTime ApplicationDate { get; set; }

    public int ApplicationTypeId { get; set; }

    public byte ApplicationStatus { get; set; }

    public DateTime LastStatusDate { get; set; }

    public decimal PaidFees { get; set; }

    public int CreatedByUserId { get; set; }

    #endregion
}


public class CreateDvldApplicationRequest
{
    #region Properties

    [Required]
    public int ApplicantPersonId { get; set; }

    [Required]
    public int ApplicationTypeId { get; set; }

    [Range(1, 3)]
    public byte ApplicationStatus { get; set; } = 1;


    [Required]
    public int CreatedByUserId { get; set; }

    #endregion
}


public class UpdateDvldApplicationRequest
{
    #region Properties

    [Required]
    public int ApplicationTypeId { get; set; }

    [Range(0, double.MaxValue)]
    public decimal PaidFees { get; set; }

    #endregion
}


public class ChangeApplicationStatusRequest
{
    #region Properties

    [Range(1, 3)]
    public byte ApplicationStatus { get; set; }

    #endregion
}