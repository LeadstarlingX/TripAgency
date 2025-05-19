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
            CreateMap<Booking, BookingDto>();
            CreateMap<CreateBookingDto, Booking>();
            CreateMap<UpdateBookingDto, Booking>();
        }

    }
}
