using Application.DTOs.Car;
using Application.DTOs;
using Application.DTOs.Posts;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class PostProfile:Profile
    {
        public PostProfile()
        {
            CreateMap<Post ,PostDto>();
            CreateMap<CreatePostDto, Post>();
            CreateMap<UpdatePostDto, Post>();
        }
    }
}
