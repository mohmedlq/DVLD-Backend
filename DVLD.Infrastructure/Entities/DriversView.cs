using System;
using System.Collections.Generic;

namespace DVLD.Infrastructure.Entities;

public partial class DriversView
{
    public int DriverId { get; set; }

    public int PersonId { get; set; }

    public string NationalNo { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public int? NumberOfActiveLicenses { get; set; }
}
