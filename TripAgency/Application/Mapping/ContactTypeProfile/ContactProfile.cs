using Application.DTOs.Contact;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping.ContactTypeProfile
{
    // ContactProfile.cs
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<ContactDto, CustomerContact>()
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CustomerContact, ContactDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ContactTypeId))
               .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

        }
    }
}
