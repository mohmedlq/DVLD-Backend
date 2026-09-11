using System;
using System.Collections.Generic;

namespace DVLD.Infrastructure.Entities;

public partial class DetainedLicensesView
{
    public int DetainId { get; set; }

    public int LicenseId { get; set; }

    public DateTime DetainDate { get; set; }

    public bool IsReleased { get; set; }

    public decimal FineFees { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public string? NationalNo { get; set; }

    public string? FullName { get; set; }

    public int? ReleaseApplicationId { get; set; }
}
