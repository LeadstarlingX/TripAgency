using Application.DTOs.CarBooking;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
using System;
using System.Collections.Generic;
using System.Linq;

using Application.DTOs.CarBooking;
using AutoMapper;
using Domain.Entities.ApplicationEntities;


namespace Application.Mapping.CarBookingProfile
{
    public class CarBookingProfile : Profile
    {
        public CarBookingProfile()
        {

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

            CreateMap<UpdateCarBookingDto, CarBooking>()
            .ForMember(dest => dest.BookingId, opt => opt.Ignore())
            .ForMember(dest => dest.CarId, opt => opt.Ignore())
            .ForMember(dest => dest.PickupLocation, opt => opt.Condition(src => src.PickupLocation != null))
            .ForMember(dest => dest.DropoffLocation, opt => opt.Condition(src => src.DropoffLocation != null))
            .ForMember(dest => dest.WithDriver, opt => opt.Condition(src => src.WithDriver.HasValue));
        }
    }
}
