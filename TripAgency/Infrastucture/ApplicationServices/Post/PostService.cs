using Application.Common;
using Application.DTOs;
using Application.DTOs.Posts;
using Application.Filter;
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

       public async  Task <PostDto> FilterPostAsync(PostFilter postFilter)
        {
            var query = await _repo.GetAllAsync();

            if(postFilter.Slug != null)
            {
                query = query.Where(x => x.Slug == postFilter.Slug);

            }
            if(postFilter.Title != null)
            {
                query =query.Where(x=>x.Title == postFilter.Title);

            }
            if(postFilter.Body != null)
            {
                query=query.Where(x=>x.Body == postFilter.Body);
            }
            if(postFilter.Summary != null)
            {
                query =query.Where(x=>x.Summary == postFilter.Summary);
            }
            if(postFilter.Views != null)
            {
                query =query.Where(x=>x.Views == postFilter.Views);
            }
            return _mapper.Map<PostDto>(query.FirstOrDefault());

        }
    }
}
