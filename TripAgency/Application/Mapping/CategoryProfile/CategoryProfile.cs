using Application.DTOs.Car;
using Application.DTOs.Category;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping.CategoryProfile
{
   public class CategoryProfile:Profile
    {

        public CategoryProfile()
        {
            CreateMap<Category,CategoryDto>();

            CreateMap<UpdateCategoryDto, Category>();

            CreateMap<CreateCategoryDto,Category>();

        }
    }
}
