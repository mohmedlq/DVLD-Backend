using System;
using System.Collections.Generic;

namespace DVLD.Infrastructure.Entities;

public partial class TestType
{
    public int TestTypeId { get; set; }

    public string TestTypeTitle { get; set; } = null!;

    public string TestTypeDescription { get; set; } = null!;

    public decimal TestTypeFees { get; set; }

    public virtual ICollection<TestAppointment> TestAppointments { get; set; } = new List<TestAppointment>();
}
