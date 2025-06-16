using Application.DTOs.Contact;
using Application.DTOs.Customer;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping.CustomerProfile
{
    public class CustomerProfile : Profile
    {
        
        public CustomerProfile()
        {
            CreateMap<CreateCustomerDto, Customer>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

            CreateMap<Customer, CustomerDto>()
                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")) 
                        .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                        .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => new ContactsDto
                        {
                            Contacts = src.Contacts.Select(c => new ContactDto
                            {
                                Id = c.Id,
                                Value = c.Value
                            }).ToList()
                        }));

            CreateMap<CustomerContact, ContactDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

            
        CreateMap<UpdateCustomerDto, Customer>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore());
        }

    }
}
