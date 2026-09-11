using System;
using System.Collections.Generic;

namespace DVLD.Infrastructure.Entities;

public partial class TestAppointmentsView
{
    public int TestAppointmentId { get; set; }

    public int LocalDrivingLicenseApplicationId { get; set; }

    public string TestTypeTitle { get; set; } = null!;

    public string ClassName { get; set; } = null!;

    public DateTime AppointmentDate { get; set; }

    public decimal PaidFees { get; set; }

    public string FullName { get; set; } = null!;

    public bool IsLocked { get; set; }
}
