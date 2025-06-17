using Application.Common;
using Application.DTOs;
using Application.DTOs.Actions;
using Application.DTOs;
using Application.DTOs.Posts;
using Application.DTOs.PostTag;
using Application.DTOs.PostType;
using Application.IApplicationServices;
using Application.IApplicationServices.PostType;
using Application.Serializer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostTagController : ControllerBase
    {

        private readonly IPostTagService _postService;
        private readonly IJsonFieldsSerializer jsonFieldsSerializer;

        public PostTagController(IJsonFieldsSerializer jsonFieldsSerializer, IPostTagService postTagService)
        {
            _postService = postTagService;
            this.jsonFieldsSerializer = jsonFieldsSerializer;
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<PostTagDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPostsTag()
        {
            var result = await _postService.GetPostTagsAsync();
            return new RawJsonActionResult(
                jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result),
                    string.Empty));
        }
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PostTagDto>), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetPostTagById(BaseDto<int> dto)
        {
            var p = await _postService.GetPostTagByIdAsync(dto);

            return new RawJsonActionResult(jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, p), string.Empty));

        }


        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<PostTagDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePostTag(UpdatePostTag dto)
        {
            var result = await _postService.UpdatePostTagAsync(dto);

            if (result == null)
            {
                return new RawJsonActionResult(
                    jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "failed update", StatusCodes.Status400BadRequest),
                        string.Empty));
            }

            return new RawJsonActionResult(
                jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "Post tag updated successfully", StatusCodes.Status200OK, result),
                    string.Empty));
        }



        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<PostTagDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePostTag(CreatePostTag dto)
        {
            var result = await _postService.CreatePostTagAsync(dto);
            if (result == null)
            {
                return new RawJsonActionResult(
                    jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "failed create", StatusCodes.Status400BadRequest),
                        string.Empty));
            }

            return new RawJsonActionResult(
                jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "Post tag created successfully", StatusCodes.Status201Created, result),
                    string.Empty));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse<PostTagDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePostTag(BaseDto<int> dto)
        {
            var result = await _postService.DeletePosTagtAsync(dto);

            if (result == null)
            {
                return new RawJsonActionResult(
                    jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "failed", StatusCodes.Status400BadRequest),
                        string.Empty));
            }

            return new RawJsonActionResult(
                jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "Post  tag  deleted successfully", StatusCodes.Status200OK),
                    string.Empty));
        }













    }
}
