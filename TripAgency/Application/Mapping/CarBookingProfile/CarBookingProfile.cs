using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.CarBooking;
using AutoMapper;
using Domain.Entities.ApplicationEntities;

namespace Application.Mapping.CarBookingProfile
{
    public class CarBookingProfile : Profile
    {
        public CarBookingProfile()
        {
            CreateMap<CarBooking, CarBookingDto>();
            CreateMap<CreateCarBookingDto, CarBooking>();
            CreateMap<UpdateCarBookingDto, CarBooking>();
        }
        
    }
}
