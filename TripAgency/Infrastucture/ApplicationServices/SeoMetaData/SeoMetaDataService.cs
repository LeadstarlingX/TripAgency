using Application.Common;
using Application.DTOs;
using Application.DTOs.SeoMetaData;
using Application.DTOs.SeoMetaDataDto;
using Application.IApplicationServices.SeoMetaData;
using Application.IReositosy;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ApplicationServices.SeoMetaData
{
    public class SeoMetaDataService : ISeoMetaDataService
    {
        private readonly IAppRepository<SEOMetaData> _repo;

        private readonly IMapper _mapper;


        public  SeoMetaDataService(IAppRepository<SEOMetaData > s ,IMapper mapper)
        {
             _repo = s;
            _mapper = mapper;
        }


        public async Task<SeoMetaDataDto> CreateSeoMetaDataAsync(CreateSeoMetaDataDto createSeoMetaDataDto)
        {
            var s = _mapper.Map<SEOMetaData>(createSeoMetaDataDto);

           await _repo.InsertAsync(s);
            return _mapper.Map<SeoMetaDataDto>(s);
        }

        public async Task<SeoMetaDataDto> DeleteSeoMetaDataAsync(BaseDto<int> dto)
        {
           var s = (await _repo.FindAsync(x=> x.Id== dto.Id)).FirstOrDefault();

            await _repo.RemoveAsync(s);
            return _mapper.Map<SeoMetaDataDto>(dto);
        }

        public async Task<IEnumerable<SeoMetaDataDto>> GetAllSeoDataAsync() => _mapper.Map<IEnumerable<SeoMetaDataDto>>(await _repo.GetAllAsync());
       

        public async Task<SeoMetaDataDto> GetSeoMetaDataByIdAsync(BaseDto<int> id)
        {
            var s = (await _repo.FindAsync(x => x.Id == id.Id)).FirstOrDefault();

            return _mapper.Map<SeoMetaDataDto>(s);

        }

        public async Task<SeoMetaDataDto> UpdateSeoMetaDtaAsync(UpdateSeoMetaDtaDto updateSeoMetaDtaDto)
        {
            var s = _mapper.Map<SEOMetaData>(updateSeoMetaDtaDto);

            await _repo.UpdateAsync(s);
            return _mapper.Map<SeoMetaDataDto>(s);
        }
    }
}
