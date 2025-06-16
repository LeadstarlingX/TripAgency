using Application.DTOs.PostType;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping.PostTypeProfile
{
    public class PostTypeProfile:Profile
    {
        public PostTypeProfile()
        {
            CreateMap<PostType ,PostTypeDto>();
            CreateMap<CreatePostTypeDto ,PostType>();
            CreateMap<UpdatePostTypeDto, PostType>();
        }
    }
}
