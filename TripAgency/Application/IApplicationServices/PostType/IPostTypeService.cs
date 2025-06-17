using Application.Common;
using Application.DTOs;
using Application.DTOs.PostType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IApplicationServices.PostType
{
    public interface IPostTypeService
    {
        Task<IEnumerable<PostTypeDto>> GetPostsTypeAsync();
        Task<PostTypeDto> CreatePostTypeAsync(CreatePostTypeDto createPostTypeDto);
        Task<PostTypeDto> UpdatePostTypeAsync(UpdatePostTypeDto updatePostTypeDto);
        Task<PostTypeDto> DeletePostTypeAsync(BaseDto<int> dto);
        Task<PostTypeDto> GetPostTypeByIdAsync(BaseDto<int> id);
    }
}
