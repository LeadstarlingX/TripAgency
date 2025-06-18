using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Credit
{
    public class CreateCreditDto
    {
        [Required]
        public long CustomerId { get; set; }
        
        [Required]
        public int PaymentMethodId { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Credit amount must be greater than 0")]
        public decimal CreditAmount { get; set; }
        
        public bool IsActive { get; set; } = true;
    }
} 