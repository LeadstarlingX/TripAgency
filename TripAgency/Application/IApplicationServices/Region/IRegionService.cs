using Application.Common;
using Application.DTOs.Region;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IApplicationServices.Region
{
    public interface IRegionService
    {
        Task<RegionDto> GetRegionByIdAsync(BaseDto<int> dto);
        Task<IEnumerable<RegionDto>> GetAllRegionsAsync();
        Task<RegionDto> CreateRegionAsync(CreateRegionDto dto);
        Task<RegionDto> UpdateRegionAsync(UpdateRegionDto dto);
        Task DeleteRegionAsync(BaseDto<int> dto);
        Task<IEnumerable<RegionDto>> GetRegionsByFilterAsync(RegionFilter? filter);

    }
}
