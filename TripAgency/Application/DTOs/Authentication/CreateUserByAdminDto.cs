using Application.DTOs.Common;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.Authentication
{
    public class CreateUserByAdminDto : BaseDto<long>
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }
       
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "Password must be at least 6 characters long.", MinimumLength = 6)]
        public required string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public required string ConfirmPassword { get; set; } // Add this for proper enum handling
        private RoleEnum Role { get; set; } = RoleEnum.Customer;
        public string Address { get; set; } = string.Empty;
    }
}
