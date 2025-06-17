using Application.DTOs.Car;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping.CarProfile
{
    public class CarProfile:Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarDto>();
            CreateMap<UpdateCarDto, Car>()          
               .ForMember(dest => dest.Capacity, opt => opt.Condition(src => src.Capacity.HasValue))
               .ForMember(dest => dest.CategoryId, opt => opt.Condition(src => src.CategoryId.HasValue))
               .ForMember(dest => dest.CarStatus, opt => opt.Condition(src => src.CarStatus.HasValue))
               .ForMember(dest => dest.Pph, opt => opt.Condition(src => src.Pph.HasValue))
               .ForMember(dest => dest.Ppd, opt => opt.Condition(src => src.Ppd.HasValue))
               .ForMember(dest => dest.Mbw, opt => opt.Condition(src => src.Mbw.HasValue))
               .ForMember(dest => dest.Model, opt => opt.Condition(src => src.Model != null))
               .ForMember(dest => dest.Color, opt => opt.Condition(src => src.Color != null))
               .ForMember(dest => dest.Image, opt => opt.Condition(src => src.Image != null));
            CreateMap<CreateCarDto, Car>();
        
        
        }
    }
}
