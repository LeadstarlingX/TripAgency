using Application.Common;
using Application.DTOs.Actions;
using Application.DTOs.Car;
using Application.DTOs;
using Application.DTOs.Posts;
using Application.IApplicationServices.Post;
using Application.Serializer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;
using Application.Filter;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        private readonly IJsonFieldsSerializer jsonFieldsSerializer;

        public PostController(IPostService postService ,IJsonFieldsSerializer jsonFieldsSerializer)
        {
             _postService = postService;
            this.jsonFieldsSerializer = jsonFieldsSerializer;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<PostDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPosts()
        {
            var result = await _postService.GetPostsAsync();
            return new RawJsonActionResult(
                jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result),
                    string.Empty));
        }
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PostDto>), StatusCodes.Status200OK)]

        public async Task<IActionResult > GetPostById(BaseDto<int> dto)
        {
            var p = await _postService.GetPostByIdAsync(dto);

            return new RawJsonActionResult(jsonFieldsSerializer.Serialize(new ApiResponse(true , "" , StatusCodes.Status200OK, p),string.Empty));

        }
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PostDto>), StatusCodes.Status200OK)]

        public async Task<IActionResult> FilterPost(PostFilter postFilter)
        {
            var p = await _postService.FilterPostAsync(postFilter);

            return new RawJsonActionResult(jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, p), string.Empty));

        }


        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<PostDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePostType(UpdatePostDto dto)
        {
            var result = await _postService.UpdatePostAsync(dto);

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
        [ProducesResponseType(typeof(ApiResponse<PostDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePost(CreatePostDto dto)
        {
            var result = await _postService.CreatePostAsync(dto);
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
        [ProducesResponseType(typeof(ApiResponse<PostDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePostType(BaseDto<int> dto)
        {
            var result = await _postService.DeletePostAsync(dto);
            if (result == null)
            {
                return new RawJsonActionResult(
                    jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "failed", StatusCodes.Status400BadRequest),
                        string.Empty));
            }

            return new RawJsonActionResult(
                jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "Post deleted successfully", StatusCodes.Status200OK),
                    string.Empty));
        }




    }
}
