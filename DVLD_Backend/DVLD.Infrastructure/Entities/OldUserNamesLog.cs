using System;
using System.Collections.Generic;

namespace DVLD.Infrastructure.Entities;

public partial class OldUserNamesLog
{
    public int LogId { get; set; }

    public string? OldUserName { get; set; }

    public string? NewUserName { get; set; }
}
