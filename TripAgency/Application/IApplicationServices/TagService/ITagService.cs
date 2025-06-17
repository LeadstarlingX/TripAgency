using Application.Common;
using Application.DTOs;
using Application.DTOs.PostType;
using Application.DTOs.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IApplicationServices.TagService
{
    public interface ITagService
    {
        Task<IEnumerable<TagDto>> GetTagAsync();
        Task<TagDto> CreateTagAsync(CreateTagDto createTagDto );
        Task<TagDto> UpdateTagAsync(UpdateTagDto updateTagDto);
        Task<TagDto> DeleteTagAsync(BaseDto<int> dto);
        Task<TagDto> GetTagByIdAsync(BaseDto<int> id);
    }
}
