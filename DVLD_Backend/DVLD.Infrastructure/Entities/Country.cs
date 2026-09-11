using System;
using System.Collections.Generic;

namespace DVLD.Infrastructure.Entities;

public partial class Country
{
    public int CountryId { get; set; }

    public string CountryName { get; set; } = null!;
}
