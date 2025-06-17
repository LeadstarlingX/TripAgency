using Application.Common;
using Application.DTOs;
using Application.DTOs.PostType;
using Application.IApplicationServices.PostType;
using Application.IReositosy;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ApplicationServices
{
    public class PostTypeService : IPostTypeService
    {
        private readonly IAppRepository<PostType> _repo;

        private readonly IMapper _mapper;

        public PostTypeService(IAppRepository<PostType> appRepository ,IMapper mapper) 
        { 
          _repo = appRepository;
            _mapper = mapper;
        }
        public async Task<PostTypeDto> CreatePostTypeAsync(CreatePostTypeDto createPostTypeDto)
        {
         var p =_mapper.Map<PostType>(createPostTypeDto);
            await _repo.InsertAsync(p);

            return _mapper.Map<PostTypeDto>(p);
        }

        public async Task<PostTypeDto> DeletePostTypeAsync(BaseDto<int> dto)
        {
           var p = (await _repo.FindAsync(x=>x.Id== dto.Id)).FirstOrDefault();
            await _repo.RemoveAsync(p);
            return _mapper.Map<PostTypeDto>(p);
        }

        public async Task<IEnumerable<PostTypeDto>> GetPostsTypeAsync()
        {
            return _mapper.Map<IEnumerable< PostTypeDto>>(await _repo.GetAllAsync());
        }

        public async Task<PostTypeDto> GetPostTypeByIdAsync(BaseDto<int> id)
        {
            return _mapper.Map<PostTypeDto>((await _repo.FindAsync(x=>x.Id== id.Id)).FirstOrDefault());
        }

        public async Task<PostTypeDto> UpdatePostTypeAsync(UpdatePostTypeDto updatePostTypeDto)
        {
            var p = _mapper.Map<PostType>(updatePostTypeDto);
            await _repo.UpdateAsync(p);

            return _mapper.Map<PostTypeDto>(p);

        }
    }
}
