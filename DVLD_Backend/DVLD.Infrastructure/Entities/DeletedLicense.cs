using System;
using System.Collections.Generic;

namespace DVLD.Infrastructure.Entities;

public partial class DeletedLicense
{
    public int DeletedId { get; set; }

    public int LicenseId { get; set; }

    public int ApplicationId { get; set; }

    public int DriverId { get; set; }

    public int LicenseClass { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public string? Notes { get; set; }

    public decimal PaidFees { get; set; }

    public bool IsActive { get; set; }

    public byte IssueReason { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime? DeletionDate { get; set; }
}
