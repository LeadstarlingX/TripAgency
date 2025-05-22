using DataAccessLayer.Enum;
using Domain.Entities.ApplicationEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreatePaymentTransactionDto
    {
        public TransactionTypeEnum TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
      //  public Payment? Payment { get; set; } // Navigation
      //    public PaymentMethod? paymentMethod { get; set; } // Navigat
    }
}
