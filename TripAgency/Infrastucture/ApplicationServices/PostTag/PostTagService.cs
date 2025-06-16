using Application.Common;
using Application.DTOs;
using Application.DTOs;
using Application.DTOs.PostTag;
using Application.IApplicationServices;
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
    public class PostTagService : IPostTagService
    {
        private readonly IAppRepository<PostTag> _repo;
        private readonly IMapper _mapper;

        public PostTagService(IAppRepository<PostTag> appRepository ,IMapper m)
        {
            _mapper = m;
            _repo = appRepository;
        }


        public async Task<PostTagDto> CreatePostTagAsync(CreatePostTag createPostTag)
        {
           var p = _mapper.Map<PostTag>(createPostTag);
            await _repo.InsertAsync(p);
            return _mapper.Map<PostTagDto>(p);
        }

        public async Task<PostTagDto> DeletePosTagtAsync(BaseDto<int> dto)
        {
            var p = (await _repo.FindAsync(x => x.TagId == dto.Id)).FirstOrDefault();

            await _repo.RemoveAsync(p);
            return _mapper.Map<PostTagDto>(p);

        }

        public async Task<PostTagDto> GetPostTagByIdAsync(BaseDto<int> id)
        {
            var p = (await _repo.FindAsync(x => x.TagId == id.Id)).FirstOrDefault();
            return _mapper.Map<PostTagDto>(p);
        }

        public async Task<IEnumerable<PostTagDto>> GetPostTagsAsync()
        {
            return _mapper.Map<IEnumerable<PostTagDto>>(await _repo .GetAllAsync());
        }

        public async Task<PostTagDto> UpdatePostTagAsync(UpdatePostTag updatPostTag)
        {
            var p =_mapper.Map<PostTag>(updatPostTag);
            await _repo.UpdateAsync(p);
            return _mapper.Map<PostTagDto>(p);
        }
    }
}
