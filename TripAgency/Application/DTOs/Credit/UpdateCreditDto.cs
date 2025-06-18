using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Credit
{
    public class UpdateCreditDto
    {
        [Required]
        public long Id { get; set; }
        
        [Range(0.01, double.MaxValue, ErrorMessage = "Credit amount must be greater than 0")]
        public decimal? CreditAmount { get; set; }
        
        public bool? IsActive { get; set; }
    }
} 