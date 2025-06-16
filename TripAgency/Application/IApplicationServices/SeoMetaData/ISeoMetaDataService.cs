using Application.Common;
using Application.DTOs;
using Application.DTOs.PostType;
using Application.DTOs.SeoMetaData;
using Application.DTOs.SeoMetaDataDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.IApplicationServices
{
    public interface ISeoMetaDataService
    {
        Task<IEnumerable<SeoMetaDataDto>> GetAllSeoDataAsync();
        Task<SeoMetaDataDto> CreateSeoMetaDataAsync(CreateSeoMetaDataDto createSeoMetaDataDto);
        Task<SeoMetaDataDto> UpdateSeoMetaDtaAsync(UpdateSeoMetaDtaDto  updateSeoMetaDtaDto);
        Task<SeoMetaDataDto> DeleteSeoMetaDataAsync(BaseDto<int> dto);
        Task<SeoMetaDataDto> GetSeoMetaDataByIdAsync(BaseDto<int> id);
        Task<IEnumerable<SeoMetaDataDto>> GetSeoMetaDataByPostID( int postId);
            }
}
