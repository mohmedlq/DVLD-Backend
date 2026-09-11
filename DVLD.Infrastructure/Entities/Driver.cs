using System;
using System.Collections.Generic;

namespace DVLD.Infrastructure.Entities;

public partial class Driver
{
    public int DriverId { get; set; }

    public int PersonId { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual User CreatedByUser { get; set; } = null!;

    public virtual ICollection<InternationalLicense> InternationalLicenses { get; set; } = new List<InternationalLicense>();

    public virtual ICollection<License> Licenses { get; set; } = new List<License>();

    public virtual Person Person { get; set; } = null!;
}
