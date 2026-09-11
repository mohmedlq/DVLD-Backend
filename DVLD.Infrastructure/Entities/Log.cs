using System;
using System.Collections.Generic;

namespace DVLD.Infrastructure.Entities;

public partial class Log
{
    public int LogId { get; set; }

    public string Level { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string? ExceptionType { get; set; }

    public string? StackTrace { get; set; }

    public string? RequestPath { get; set; }

    public string? HttpMethod { get; set; }

    public int? UserId { get; set; }

    public DateTime CreatedAt { get; set; }
}
