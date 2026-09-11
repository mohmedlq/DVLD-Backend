using DVLD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Application.DTOs
{
    public class UserDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]

        public int PersonId { get;  set; }
        [Required]

        public string UserName { get; set; } = null!;
        public bool IsActive { get; set; }
        

    }
    public class CreateUserRequest
    {
        [Required]
        public int PersonId { get; set; }
        
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
      
        public bool IsActive { get; set; }
    }
    public class UpdateUserRequest
    {
        [Required]
        public string UserName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
    public class ChangePasswordRequest
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string NewPassword { get; set; } = null!;

    }
}
