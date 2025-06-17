using Application.Common;
using Application.DTOs;
using Application.DTOs.Tag;
using Application.IApplicationServices;
using Application.IApplicationServices;
using Application.IApplicationServices;
using Application.IApplicationServices.TagService;
using Application.IReositosy;
using AutoMapper;
using Domain.Entities.ApplicationEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ApplicationServices
{
    public class TagService : ITagService
    {

        private readonly IAppRepository<Tag> _repo;
        private readonly IMapper _mapper;


        public TagService(IAppRepository<Tag> appRepository, IMapper mapper)
        { 
            _mapper = mapper;
            _repo = appRepository;

        }




        public async Task<TagDto> CreateTagAsync(CreateTagDto createTagDto)
        {
            var t = _mapper.Map<Tag>(createTagDto);
            await _repo.InsertAsync(t);
            return _mapper.Map<TagDto>(t);
        }

        public async Task<TagDto> DeleteTagAsync(BaseDto<int> dto)
        {
           var t =(await _repo.FindAsync(x => x.Id == dto.Id)).FirstOrDefault();
            await _repo.RemoveAsync(t);
            return _mapper.Map<TagDto>(t);
        }

        public async Task<IEnumerable<TagDto>> GetTagAsync()
        {
            return _mapper.Map<IEnumerable<TagDto>>(await _repo.GetAllAsync());
        }

        public async Task<TagDto> GetTagByIdAsync(BaseDto<int> id)
        {
            var t = (await _repo.FindAsync(x => x.Id == id.Id)).FirstOrDefault();
            return _mapper.Map<TagDto>(t);
        }

        public async Task<TagDto> UpdateTagAsync(UpdateTagDto updateTagDto)
        {
            var t = _mapper.Map<Tag>(updateTagDto);

            await _repo.UpdateAsync(t);
            return _mapper.Map<TagDto>(t);
        }
    }
}
