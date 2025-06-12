using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Booking;
using AutoMapper;
using Domain.Entities.ApplicationEntities;

namespace Application.Mapping.BookingProfile
{
    public class BookingProfie : Profile
    {
        public BookingProfie()
        {
            CreateMap<Booking, BookingDto>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int)src.Id))

             .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src =>
                 src.Customer != null ? src.Customer.FirstName+" "+src.Customer.LastName : "N/A"))

             .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src =>
                 src.Employee != null ? src.Employee.Name : "N/A"));
            CreateMap<CreateBookingDto, Booking>()
                      .ForMember(dest => dest.Status, opt => opt.Ignore());
            CreateMap<UpdateBookingDto, Booking>()
                        .ForMember(dest => dest.Id, opt => opt.Ignore());
        }

    }
}
