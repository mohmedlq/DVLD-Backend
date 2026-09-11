using System.ComponentModel.DataAnnotations;

namespace DVLD.Application.DTOs;

public class DetainedLicenseDto
{
    #region Properties

    public int DetainId { get; set; }

    public int LicenseId { get; set; }

    public DateTime DetainDate { get; set; }

    public decimal FineFees { get; set; }

    public int CreatedByUserId { get; set; }

    public bool IsReleased { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public int? ReleasedByUserId { get; set; }

    public int? ReleaseApplicationId { get; set; }

    #endregion
}


public class CreateDetainedLicenseRequest
{
    #region Properties

    [Required]
    public int LicenseId { get; set; }

    [Range(0, double.MaxValue)]
    public decimal FineFees { get; set; }

    [Required]
    public int CreatedByUserId { get; set; }

    #endregion
}


public class UpdateDetainedLicenseRequest
{
    #region Properties

    [Range(0, double.MaxValue)]
    public decimal FineFees { get; set; }

    #endregion
}


public class ReleaseDetainedLicenseRequest
{
    #region Properties

    [Required]
    public int ReleasedByUserId { get; set; }

    public int? ReleaseApplicationId { get; set; }

    #endregion
}