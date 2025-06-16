using Application.Common;
using Application.DTOs.Actions;
using Application.DTOs;
using Application.DTOs.Posts;
using Application.DTOs.PostType;
using Application.IApplicationServices.Post;
using Application.IApplicationServices.PostType;
using Application.Serializer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostTypeController : ControllerBase
    {
        private readonly IPostTypeService _postService;
        private readonly IJsonFieldsSerializer jsonFieldsSerializer;

        public PostTypeController(IJsonFieldsSerializer jsonFieldsSerializer , IPostTypeService postTypeService)
        {
            _postService = postTypeService;
            this.jsonFieldsSerializer = jsonFieldsSerializer;
        }
       

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<PostTypeDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPostsType()
        {
            var result = await _postService.GetPostsTypeAsync();
            return new RawJsonActionResult(
                jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result),
                    string.Empty));
        }
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PostTypeDto>), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetPostTypeById(BaseDto<int> dto)
        {
            var p = await _postService.GetPostTypeByIdAsync(dto);

            return new RawJsonActionResult(jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, p), string.Empty));

        }


        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<PostTypeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePostType(UpdatePostTypeDto dto)
        {
            var result = await _postService.UpdatePostTypeAsync(dto);

            if (result == null)
            {
                return new RawJsonActionResult(
                    jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "failed update", StatusCodes.Status400BadRequest),
                        string.Empty));
            }

            return new RawJsonActionResult(
                jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "Post updated successfully", StatusCodes.Status200OK, result),
                    string.Empty));
        }



        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<PostTypeDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePostType(CreatePostTypeDto dto)
        {
            var result = await _postService.CreatePostTypeAsync(dto);
            if (result == null)
            {
                return new RawJsonActionResult(
                    jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "failed create", StatusCodes.Status400BadRequest),
                        string.Empty));
            }

            return new RawJsonActionResult(
                jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "Post Type created successfully", StatusCodes.Status201Created, result),
                    string.Empty));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse<PostTypeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePostType(BaseDto<int> dto)
        {
            var result = await _postService.DeletePostTypeAsync(dto);

            if (result == null)
            {
                return new RawJsonActionResult(
                    jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "failed", StatusCodes.Status400BadRequest),
                        string.Empty));
            }

            return new RawJsonActionResult(
                jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "Post  Type  deleted successfully", StatusCodes.Status200OK),
                    string.Empty));
        }

    }
}
