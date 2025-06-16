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
    public class TagProfile:Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagDto>();
            CreateMap<CreateTagDto, Tag>();
            CreateMap<UpdateTagDto, Tag>();
        }
    }
}
