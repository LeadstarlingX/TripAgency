using Application.DTOs;
using Application.DTOs.PaymentTransaction;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping.PaymentTransactionProfile
{
    public class PaymentTransactionProfile:Profile
    {
        public PaymentTransactionProfile()
        {
            CreateMap<PaymentTransaction,PaymentTransactionDto>();
            CreateMap<CreatePaymentTransactionDto,PaymentTransaction>();
            CreateMap<UpdatePaymentTransactionDto,PaymentTransaction>();
        }
    }
}
