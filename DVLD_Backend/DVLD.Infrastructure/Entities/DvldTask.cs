using System;
using System.Collections.Generic;

namespace DVLD.Infrastructure.Entities;

public partial class DvldTask
{
    public int TaskId { get; set; }

    public int UserId { get; set; }

    public string TaskTitle { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateOnly AssignedDate { get; set; }

    public DateOnly DueDate { get; set; }

    public bool Status { get; set; }

    public virtual User User { get; set; } = null!;
}
