using System;
using System.Collections.Generic;

namespace DVLD.Infrastructure.Entities;

public partial class Massage
{
    public int MessageId { get; set; }

    public int PersonId { get; set; }

    public string MessageTitle { get; set; } = null!;

    public string MessageContent { get; set; } = null!;

    public bool IsRead { get; set; }

    public virtual Person Person { get; set; } = null!;
}
