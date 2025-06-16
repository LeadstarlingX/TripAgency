using Application.Common;
using Application.DTOs;
using Application.DTOs.Posts;
using Application.IApplicationServices;
using Application.IApplicationServices.Post;
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
    public class PostService : IPostService
    {
        private readonly IAppRepository<Post> _repo;

        private readonly IMapper _mapper;

        public PostService(IMapper mapper ,IAppRepository<Post> p)
        {
            _repo = p;
            _mapper = mapper;
        }
         public async  Task<PostDto> CreatePostAsync(CreatePostDto createPostDto)
        {
            var p = _mapper.Map<Post>(createPostDto);

            await _repo.InsertAsync(p);
            return _mapper.Map<PostDto>(p);
        }

        public async Task<PostDto> DeletePostAsync(BaseDto<int> dto)
        {
           var p = (await _repo.FindAsync(x => x.Id == dto.Id)).FirstOrDefault();
            await _repo.RemoveAsync(p);

            return _mapper.Map<PostDto>(p);
        }

       

        public async Task<PostDto> GetPostByIdAsync(BaseDto<int> id)
        {
            var p = (await _repo.FindAsync(x => x.Id == id.Id)).FirstOrDefault();
            return _mapper.Map<PostDto>(p);

           
        }

        public async Task<PostDto> UpdatePostAsync(UpdatePostDto updatePostDto)
        {
            var p =_mapper.Map<Post> (updatePostDto);
            await _repo.UpdateAsync(p);
            return _mapper.Map<PostDto>(p);
        }

       public async Task<IEnumerable<PostDto>> GetPostsAsync()
        {
            return _mapper.Map<IEnumerable<PostDto>>(await _repo.GetAllAsync());
        }
    }
}
