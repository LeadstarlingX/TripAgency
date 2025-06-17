using Application.DTOs.SeoMetaData;
using Application.DTOs.SeoMetaDataDto;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping.SeoMetaData
{
    public class SeoMetaDataProfile:Profile
    {

        public SeoMetaDataProfile()
        {
             CreateMap<SEOMetaData,SeoMetaDataDto>();
            CreateMap<CreateSeoMetaDataDto, SEOMetaData>();
            CreateMap<UpdateSeoMetaDtaDto ,SEOMetaData>();
        }
    }


}
