using Application.Common;
using Application.DTOs;
using Application.DTOs;
using Application.DTOs.Posts;
using Application.DTOs.PostTag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IApplicationServices
{
    public interface IPostTagService
    {
        Task<IEnumerable<PostTagDto>> GetPostTagsAsync();
        Task<PostTagDto> CreatePostTagAsync(CreatePostTag createPostTag );
        Task<PostTagDto> UpdatePostTagAsync( UpdatePostTag updatPostTag );
        Task<PostTagDto> DeletePosTagtAsync(BaseDto<int> dto);
        Task<PostTagDto> GetPostTagByIdAsync(BaseDto<int> id);







    }
}
