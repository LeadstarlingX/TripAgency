using Application.DTOs.Payment;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping.PaymentProfile
{
    public class PaymentProfile:Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment ,PaymentDto>();
            CreateMap<UpdatePaymentDto, Payment>();
            CreateMap<CreatePaymentDto, Payment>();
        }
    }
}
