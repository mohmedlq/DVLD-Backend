using System;
using System.Collections.Generic;

namespace DVLD.Infrastructure.Entities;

public partial class User
{
    public int UserId { get; set; }

    public int PersonId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;
    
    public bool IsActive { get; set; }

    public virtual ICollection<DvldApplication> Applications { get; set; } = new List<DvldApplication>();

    public virtual ICollection<DetainedLicense> DetainedLicenseCreatedByUsers { get; set; } = new List<DetainedLicense>();

    public virtual ICollection<DetainedLicense> DetainedLicenseReleasedByUsers { get; set; } = new List<DetainedLicense>();

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();

    public virtual ICollection<InternationalLicense> InternationalLicenses { get; set; } = new List<InternationalLicense>();

    public virtual ICollection<License> Licenses { get; set; } = new List<License>();

    public virtual Person Person { get; set; } = null!;

    public virtual ICollection<DvldTask> Tasks { get; set; } = new List<DvldTask>();

    public virtual ICollection<TestAppointment> TestAppointments { get; set; } = new List<TestAppointment>();

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
