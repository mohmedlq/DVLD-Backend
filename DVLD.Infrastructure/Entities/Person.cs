using System;
using System.Collections.Generic;

namespace DVLD.Infrastructure.Entities;

public partial class Person
{
    public int PersonId { get; set; }

    public string? NationalNo { get; set; }

    public string? FirstName { get; set; }

    public string? SecondName { get; set; }

    public string? ThirdName { get; set; }

    public string? LastName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// 0 Male , 1 Femail
    /// </summary>
    public byte? Gender { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public int? CountryId { get; set; }

    public string? ImagePath { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<DvldApplication> Applications { get; set; } = new List<DvldApplication>();

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();

    public virtual ICollection<Massage> Massages { get; set; } = new List<Massage>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
