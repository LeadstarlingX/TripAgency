using Application.Common;
using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Authentication
{
    public class UserProfileDto : BaseDto<long>
    {
        public TokenDto? Token { get; set; }
        public required string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

    }
}
