using System;
using System.Collections.Generic;

namespace DVLD.Infrastructure.Entities;

public partial class InternationalLicense
{
    public int InternationalLicenseId { get; set; }

    public int ApplicationId { get; set; }

    public int DriverId { get; set; }

    public int IssuedUsingLocalLicenseId { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public bool IsActive { get; set; }

    public int CreatedByUserId { get; set; }

    public virtual DvldApplication Application { get; set; } = null!;

    public virtual User CreatedByUser { get; set; } = null!;

    public virtual Driver Driver { get; set; } = null!;

    public virtual License IssuedUsingLocalLicense { get; set; } = null!;
}
