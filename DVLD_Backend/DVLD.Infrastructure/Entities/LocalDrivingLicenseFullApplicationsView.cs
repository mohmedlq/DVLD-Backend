using System;
using System.Collections.Generic;

namespace DVLD.Infrastructure.Entities;

public partial class LocalDrivingLicenseFullApplicationsView
{
    public int ApplicationId { get; set; }

    public int ApplicantPersonId { get; set; }

    public DateTime ApplicationDate { get; set; }

    public int ApplicationTypeId { get; set; }

    public byte ApplicationStatus { get; set; }

    public DateTime LastStatusDate { get; set; }

    public decimal PaidFees { get; set; }

    public int CreatedByUserId { get; set; }

    public int LocalDrivingLicenseApplicationId { get; set; }

    public int LicenseClassId { get; set; }
}
