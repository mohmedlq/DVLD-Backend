using System;
using System.Collections.Generic;

namespace DVLD.Infrastructure.Entities;

public partial class LocalDrivingLicenseApplicationsView
{
    public int LocalDrivingLicenseApplicationId { get; set; }

    public string ClassName { get; set; } = null!;

    public string NationalNo { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public DateTime ApplicationDate { get; set; }

    public int? PassedTestCount { get; set; }

    public string? Status { get; set; }
}
