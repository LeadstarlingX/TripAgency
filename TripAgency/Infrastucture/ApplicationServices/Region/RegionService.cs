using Application.Common;
using Application.DTOs.Region;
using Application.IApplicationServices.Region;
using Application.IReositosy;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ApplicationServices.Region
{
    public class RegionService : IRegionService
    {
        private readonly IAppRepository<Domain.Entities.ApplicationEntities.Region> _regionRepository;
        private readonly IMapper _mapper;

        public RegionService(IAppRepository<Domain.Entities.ApplicationEntities.Region> regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        public async Task<RegionDto> GetRegionByIdAsync(BaseDto<int> dto)
        {
            var region = (await _regionRepository.FindAsync(r => r.Id == dto.Id)).FirstOrDefault();
            if (region == null)
                throw new KeyNotFoundException($"Region with ID {dto.Id} not found.");
            return _mapper.Map<RegionDto>(region);
        }

        public async Task<IEnumerable<RegionDto>> GetAllRegionsAsync()
        {
            var regions = (await _regionRepository.GetAllAsync()).ToList();
            return _mapper.Map<IEnumerable<RegionDto>>(regions);
        }

        public async Task<RegionDto> CreateRegionAsync(CreateRegionDto dto)
        {
            var regionEntity = _mapper.Map<Domain.Entities.ApplicationEntities.Region>(dto);
            var newRegion = await _regionRepository.InsertAsync(regionEntity);
            return _mapper.Map<RegionDto>(newRegion);
        }

        public async Task<RegionDto> UpdateRegionAsync(UpdateRegionDto dto)
        {
            var existingRegion =( await _regionRepository.FindAsync(r => r.Id == dto.Id)).FirstOrDefault();
            if (existingRegion == null)
                throw new KeyNotFoundException($"Region with ID {dto.Id} not found.");

            _mapper.Map(dto, existingRegion);
            await _regionRepository.UpdateAsync(existingRegion);
            return _mapper.Map<RegionDto>(existingRegion);
        }

        public async Task DeleteRegionAsync(BaseDto<int> dto)
        {
            var regionToDelete = (await _regionRepository.FindAsync(r => r.Id == dto.Id)).FirstOrDefault();
            if (regionToDelete == null)
                throw new KeyNotFoundException($"Region with ID {dto.Id} not found.");

            await _regionRepository.RemoveAsync(regionToDelete);
        }

        public async Task<IEnumerable<RegionDto>> GetRegionsByFilterAsync(RegionFilter? filter)
        {
            var query = _regionRepository.GetAll();

            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.Name))
                {
                    query = query.Where(r => r.Name.Trim().ToLower().Contains(filter.Name.Trim().ToLower()));
                }
            }

            var regions = await query.ToListAsync();
            return _mapper.Map<IEnumerable<RegionDto>>(regions);
        }
    }
}
