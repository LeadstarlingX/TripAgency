using Application.Common;
using Application.DTOs.Actions;
using Application.DTOs.Credit;
using Application.IApplicationServices.Authentication;
using Application.IApplicationServices.Credit;
using Application.Serializer;
using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = ApiConsts.AdminRoleName)]
    public class CreditController : BaseAuthenticatedController
    {
        private readonly ICreditService _creditService;
        
        public CreditController(
            IAuthenticationService authenticationService,
            IJsonFieldsSerializer jsonFieldsSerializer,
            ICreditService creditService) : base(authenticationService, jsonFieldsSerializer)
        {
            _creditService = creditService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<CreditsDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCreditsAsync()
        {
            var result = await _creditService.GetCreditsAsync();
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<CreditDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCreditByIdAsync([FromQuery] BaseDto<long> dto)
        {
            var result = await _creditService.GetCreditByIdAsync(dto);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CreditDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCreditAsync([FromBody] CreateCreditDto createCreditDto)
        {
            var result = await _creditService.CreateCreditAsync(createCreditDto);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "Credit created successfully", StatusCodes.Status201Created, result), string.Empty));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<CreditDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCreditAsync([FromBody] UpdateCreditDto updateCreditDto)
        {
            var result = await _creditService.UpdateCreditAsync(updateCreditDto);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "Credit updated successfully", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCreditAsync([FromBody] BaseDto<long> dto)
        {
            await _creditService.DeleteCreditAsync(dto);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "Credit deleted successfully", StatusCodes.Status200OK), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<CreditsDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCreditsByCustomerAsync([FromQuery] BaseDto<long> customerDto)
        {
            var result = await _creditService.GetCreditsByCustomerAsync(customerDto);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<CreditsDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCreditsByPaymentMethodAsync([FromQuery] BaseDto<int> paymentMethodDto)
        {
            var result = await _creditService.GetCreditsByPaymentMethodAsync(paymentMethodDto);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }
    }
} 