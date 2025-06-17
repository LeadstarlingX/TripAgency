using Application.DTOs.Car;
using Application.DTOs;
using Application.DTOs.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Filter;

namespace Application.IApplicationServices.Post
{
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> GetPostsAsync();
        Task<PostDto> CreatePostAsync(CreatePostDto createPostDto);
        Task<PostDto> UpdatePostAsync(UpdatePostDto updatePostDto);
        Task<PostDto> DeletePostAsync(BaseDto<int> dto);
        Task<PostDto> GetPostByIdAsync(BaseDto<int> id);

        Task<PostDto> FilterPostAsync(PostFilter postFilter);
    }
}
