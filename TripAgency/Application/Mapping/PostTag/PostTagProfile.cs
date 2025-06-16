using Application.DTOs.PostTag;
using Application.DTOs.Tag;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class PostTagProfile:Profile
    {

        public PostTagProfile()
        {
            CreateMap<PostTag, PostTagDto>();
            CreateMap<CreateTagDto, PostTag>();

            CreateMap<UpdateTagDto , PostTag>();
        }
    }
}
