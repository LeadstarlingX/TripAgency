using Application.DTOs.Employee;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping.EmployeeProfile
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));

            CreateMap<UpdateEmployeeDto, Employee>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.HireDate, opt => opt.Condition(src => src.HireDate.HasValue))
                .ForMember(dest => dest.Name, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Name)))
                .ForMember(dest => dest.Address, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Address)));

            CreateMap<Employee, EmployeeDto>();
        }
    }
}
