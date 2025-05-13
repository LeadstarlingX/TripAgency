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
            CreateMap<UpdateCarDto, Car>();
            CreateMap<CreateCarDto, Car>();
        
        
        }
    }
}
