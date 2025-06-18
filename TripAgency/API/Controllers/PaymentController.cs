using Application.Common;
using Application.DTOs.Actions;
using Application.DTOs.Payment;
using Application.DTOs.PaymentMethod;
using Application.Filter;
using Application.IApplicationServices.Payment;
using Application.Serializer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;

        public PaymentController(IPaymentService paymentService 
            ,IJsonFieldsSerializer jsonFieldsSerializer)
        {
             _paymentService = paymentService;
            _jsonFieldsSerializer = jsonFieldsSerializer;
            

        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<PaymentDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPayments()
        {
            var Payment = await _paymentService.GetPaymentsAsync();
            return new RawJsonActionResult(
            _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, Payment),
                    string.Empty));

        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<PaymentDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPaymentsByFilter([FromQuery] PaymentFilter? filter)
        {
            var payments = await _paymentService.GetPaymentsByFilterAsync(filter);
            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, payments),
                    string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PaymentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPaymentById(BaseDto<int> dto)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(dto);
            if (payment is null)
            {
                return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "Payment not found", StatusCodes.Status404NotFound),
                        string.Empty));
            }
            return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK, payment),
                        string.Empty));

        }
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<PaymentDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePayment(CreatePaymentDto dto)
        {
            var result = await _paymentService.CreatePaymentAsync(dto);

            if (result is null)
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
        public async Task<IActionResult> DeletePayment(BaseDto<int> dto)
        {
            var result = await _paymentService.DeletePaymentAsync(dto);
            if (result is null)
            {
                return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "failed", StatusCodes.Status400BadRequest),
                        string.Empty));
            }

            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "deleted successfully", StatusCodes.Status200OK),
                    string.Empty));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<PaymentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePayment(UpdatePaymentDto dto)
        {
            var result = await _paymentService.UpdatePayment(dto);
            if (result is null)
            {
                return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "failed update", StatusCodes.Status400BadRequest),
                        string.Empty));
            }

            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, " updated successfully", StatusCodes.Status200OK, result),
                    string.Empty));
        }
        
    }

}     