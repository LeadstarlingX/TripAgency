using Application.Common;
using Application.DTOs.Actions;
using Application.DTOs;
using Application.DTOs.Posts;
using Application.DTOs.PostType;
using Application.DTOs.SeoMetaData;
using Application.DTOs.SeoMetaDataDto;
using Application.IApplicationServices.SeoMetaData;
using Application.Serializer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SeoMetaDataController : ControllerBase
    {
        private readonly ISeoMetaDataService _service;

        private readonly IJsonFieldsSerializer jsonFieldsSerializer;

        public SeoMetaDataController(ISeoMetaDataService seoMetaDataService, IJsonFieldsSerializer jsonFieldsSerializer)
        {
           this.jsonFieldsSerializer = jsonFieldsSerializer;
            _service = seoMetaDataService;

        }
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<SeoMetaDataDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSeoMetaData()
        {
            var result = await _service.GetAllSeoDataAsync();
            return new RawJsonActionResult(
                jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, result),
                    string.Empty));
        }
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<SeoMetaDataDto>), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetSeoMetaById(BaseDto<int> dto)
        {
            var p = await _service.GetSeoMetaDataByIdAsync(dto);

            return new RawJsonActionResult(jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, p), string.Empty));

        }


        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<SeoMetaDataDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSeoMetaData(UpdateSeoMetaDtaDto dto)
        {
            var result = await _service.UpdateSeoMetaDtaAsync(dto);

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
        [ProducesResponseType(typeof(ApiResponse<SeoMetaDataDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSeoMetaDataType(CreateSeoMetaDataDto dto)
        {
            var result = await _service.CreateSeoMetaDataAsync(dto);
            if (result == null)
            {
                return new RawJsonActionResult(
                    jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "failed create", StatusCodes.Status400BadRequest),
                        string.Empty));
            }

            return new RawJsonActionResult(
                jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "  created successfully", StatusCodes.Status201Created, result),
                    string.Empty));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse<SeoMetaDataDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteSeoMetaData(BaseDto<int> dto)
        {
            var result = await _service.DeleteSeoMetaDataAsync(dto);

            if (result == null)
            {
                return new RawJsonActionResult(
                    jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "failed", StatusCodes.Status400BadRequest),
                        string.Empty));
            }

            return new RawJsonActionResult(
                jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "    deleted successfully", StatusCodes.Status200OK),
                    string.Empty));
        }

    }
}
