using Application.DTOs.Actions;
using Application.DTOs;
using Application.DTOs.Posts;
using Application.DTOs.Tag;
using Application.IApplicationServices;
using Application.IApplicationServices.Post;
using Application.Serializer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.IApplicationServices.TagService;
using Application.Common;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        private readonly IJsonFieldsSerializer jsonFieldsSerializer;

        public TagController(ITagService TagService, IJsonFieldsSerializer jsonFieldsSerializer)
        {
             _tagService = TagService;
            this.jsonFieldsSerializer = jsonFieldsSerializer;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<TagDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTags()
        {
            var result = await _tagService.GetTagAsync();
            return new RawJsonActionResult(
                jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result),
                    string.Empty));
        }
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<TagDto>), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetTagById(BaseDto<int> dto)
        {
            var p = await _tagService.GetTagByIdAsync(dto);

            return new RawJsonActionResult(jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, p), string.Empty));

        }


        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<TagDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTag(UpdateTagDto dto)
        {
            var result = await _tagService.UpdateTagAsync(dto);

            if (result == null)
            {
                return new RawJsonActionResult(
                    jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "failed update", StatusCodes.Status400BadRequest),
                        string.Empty));
            }

            return new RawJsonActionResult(
                jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, " updated successfully", StatusCodes.Status200OK, result),
                    string.Empty));
        }



        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<TagDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTag(CreateTagDto dto)
        {
            var result = await _tagService.CreateTagAsync(dto);
            if (result == null)
            {
                return new RawJsonActionResult(
                    jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "failed create", StatusCodes.Status400BadRequest),
                        string.Empty));
            }

            return new RawJsonActionResult(
                jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "Post created successfully", StatusCodes.Status201Created, result),
                    string.Empty));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse<TagDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTag(BaseDto<int> dto)
        {
            var result = await  _tagService.DeleteTagAsync(dto);
            if (result == null)
            {
                return new RawJsonActionResult(
                    jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "failed", StatusCodes.Status400BadRequest),
                        string.Empty));
            }

            return new RawJsonActionResult(
                jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, " deleted successfully", StatusCodes.Status200OK),
                    string.Empty));
        }

    }
}
