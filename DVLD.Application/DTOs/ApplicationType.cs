using System.ComponentModel.DataAnnotations;

namespace DVLD.Application.DTOs;

public class ApplicationTypeDto
{

    public int ApplicationTypeId { get; set; }

    public string ApplicationTypeTitle { get; set; } = null!;

    public decimal ApplicationFees { get; set; }

}


public class CreateApplicationTypeRequest
{

    [Required]
    public string ApplicationTypeTitle { get; set; } = null!;

    [Range(0, double.MaxValue)]
    public decimal ApplicationFees { get; set; }

    
}


public class UpdateApplicationTypeRequest
{

    [Required]
    public string ApplicationTypeTitle { get; set; } = null!;

    [Range(0, double.MaxValue)]
    public decimal ApplicationFees { get; set; }

    
}