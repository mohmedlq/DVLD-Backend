using System;
using System.Collections.Generic;

namespace DVLD.Infrastructure.Entities;

public partial class LocalDrivingLicenseApplication
{
    public int LocalDrivingLicenseApplicationId { get; set; }

    public int ApplicationId { get; set; }

    public int LicenseClassId { get; set; }

    public virtual DvldApplication Application { get; set; } = null!;

    public virtual LicenseClass LicenseClass { get; set; } = null!;

    public virtual ICollection<TestAppointment> TestAppointments { get; set; } = new List<TestAppointment>();
}
