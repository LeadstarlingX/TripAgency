using Application.DTOs.PaymentMethod;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping.PymentMethodProfile
{
   public class PaymentMethodProfile:Profile
    {
        public PaymentMethodProfile()
        {
            CreateMap<CreatePaymentMethodDto, PaymentMethod>();
            CreateMap<UpdatePaymentMethodDto, PaymentMethod>();
            CreateMap<PaymentMethod, PaymentMethodDto>();
        }
    }
}
