using DVLD.Domain.Entities;

namespace DVLD.Application.DTOs;

public class DriverDto
{
    public int DriverId { get; set; }

    public int PersonId { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }
}

public class CreateDriverRequest
{
    public int PersonId { get; set; }

    public int CreatedByUserId { get; set; }
}

public class UpdateDriverRequest
{
    public int PersonId { get; set; }

    public int CreatedByUserId { get; set; }
}