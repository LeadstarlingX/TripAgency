<<<<<<< HEAD
﻿using System;
=======
﻿using Application.DTOs.CarBooking;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
using System;
>>>>>>> AlaaWork
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
<<<<<<< HEAD
using Application.DTOs.CarBooking;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
=======
>>>>>>> AlaaWork

namespace Application.Mapping.CarBookingProfile
{
    public class CarBookingProfile : Profile
    {
        public CarBookingProfile()
        {
<<<<<<< HEAD
            CreateMap<CarBooking, CarBookingDto>();
            CreateMap<CreateCarBookingDto, CarBooking>();
            CreateMap<UpdateCarBookingDto, CarBooking>();
        }
        
=======
            CreateMap<CarBooking, CarBookingDto>()
            .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.BookingId))
            .ForMember(dest => dest.CarId, opt => opt.MapFrom(src => src.CarId))
            .ForMember(dest => dest.PickupLocation, opt => opt.MapFrom(src => src.PickupLocation))
            .ForMember(dest => dest.DropoffLocation, opt => opt.MapFrom(src => src.DropoffLocation))
            .ForMember(dest => dest.WithDriver, opt => opt.MapFrom(src => src.WithDriver))
            .ForMember(dest => dest.Booking, opt => opt.MapFrom(src => src.Booking))
            .ForMember(dest => dest.Car, opt => opt.MapFrom(src => src.Car)); 

            CreateMap<CarBookingDto, CarBooking>()
               .ForMember(dest => dest.Booking, opt => opt.Ignore())
               .ForMember(dest => dest.Car, opt => opt.Ignore())      
               .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.BookingId))  
               .ForMember(dest => dest.CarId, opt => opt.MapFrom(src => src.CarId))
               .ForMember(dest => dest.PickupLocation, opt => opt.MapFrom(src => src.PickupLocation))
               .ForMember(dest => dest.DropoffLocation, opt => opt.MapFrom(src => src.DropoffLocation))
               .ForMember(dest => dest.WithDriver, opt => opt.MapFrom(src => src.WithDriver));
        }
>>>>>>> AlaaWork
    }
}
