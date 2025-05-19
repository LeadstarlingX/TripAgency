using Application.DTOs.Actions;
using Application.DTOs.Car;
using Application.DTOs.Common;
using Application.DTOs.PaymentMethod;
using Application.IApplicationServices;
using Application.Serializer;
using Domain.Entities.ApplicationEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;
        public PaymentMethodController(IPaymentMethodService paymentMethodService 
            ,IJsonFieldsSerializer jsonFieldsSerializer)
        {
          _paymentMethodService = paymentMethodService; 
            _jsonFieldsSerializer = jsonFieldsSerializer;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<PaymentMethodDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPaymentMethod()
        {
            var PaymentMethods = await _paymentMethodService.GetPaymentMethodsAsync();
            return new RawJsonActionResult(
            _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, PaymentMethods),
                    string.Empty));

        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PaymentMethodDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPaymentMethodById(BaseDto<int> dto)
        {
            var paymentmetod = await _paymentMethodService.GetPaymentMethodByIdAsync(dto);
            if(paymentmetod == null)
            {
                return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "Car not found", StatusCodes.Status404NotFound),
                        string.Empty));
            }
            return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK,paymentmetod),
                        string.Empty));

        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<PaymentMethodDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePaymentMethod(CreatePaymentMethodDto dto)
        {
            var result = await _paymentMethodService.CreatePaymentMethodAsync(dto);

            if (result == null)
            {
                return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "failed create", StatusCodes.Status400BadRequest),
                        string.Empty));
            }

            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "Car created successfully", StatusCodes.Status201Created, result),
                    string.Empty));
        }


        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePaymentMethod(BaseDto<int> dto)
        {
            var result = await _paymentMethodService.DeletePaymentMethodAsync(dto);
            if (result == null)
            {
                return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "failed", StatusCodes.Status400BadRequest),
                        string.Empty));
            }

            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "Car deleted successfully", StatusCodes.Status200OK),
                    string.Empty));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<PaymentMethodDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePaymentMethod(UpdatePaymentMethodDto dto)
        {
            var result = await _paymentMethodService.UpdatePaymentMethod(dto);
            if (result == null)
            {
                return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "failed update", StatusCodes.Status400BadRequest),
                        string.Empty));
            }

            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "Car updated successfully", StatusCodes.Status200OK, result),
                    string.Empty));
        }








    }
}
