using Application.Common;
using Application.DTOs;
using Application.DTOs.Actions;
using Application.DTOs.PaymentMethod;
using Application.DTOs.PaymentTransaction;

using Application.IApplicationServices;
using Application.IApplicationServices.PaymentTransaction;
using Application.Serializer;
using Infrastructure.ApplicationServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentTransactionController : ControllerBase
    {
        private readonly IPaymentTransactionService _paymenttransactionService;
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;
        public PaymentTransactionController(IPaymentTransactionService paymenttransactionService
            , IJsonFieldsSerializer jsonFieldsSerializer)
        {
             _paymenttransactionService = paymenttransactionService;
            _jsonFieldsSerializer = jsonFieldsSerializer;
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<PaymentTransactionDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPaymentTransaction()
        {
            var PaymentTransactions = await _paymenttransactionService.GetPaymentTransactionsAsync();
            return new RawJsonActionResult(
            _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "", StatusCodes.Status200OK, PaymentTransactions),
                    string.Empty));

        }






        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PaymentTransactionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPaymentTransactionById(BaseDto<int> dto)
        {
            var paymenttransaction = await _paymenttransactionService.GetPaymentTransactionByIdAsync(dto);
            if (paymenttransaction is null)
            {
                return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "Car not found", StatusCodes.Status404NotFound),
                        string.Empty));
            }
            return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(true, "", StatusCodes.Status200OK,paymenttransaction),
                        string.Empty));

        }



        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<PaymentTransactionDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePaymentTransaction(CreatePaymentTransactionDto dto)
        {
            var result = await _paymenttransactionService.CreatePaymentTransactionAsync(dto);
            if (result is null)
            {
                return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "failed create", StatusCodes.Status400BadRequest),
                        string.Empty));
            }

            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "created successfully", StatusCodes.Status201Created, result),
                    string.Empty));
        }



        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePaymentTransaction(BaseDto<int> dto)
        {
            var result = await _paymenttransactionService.DeletePaymentTransactionAsync(dto);
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
        [ProducesResponseType(typeof(ApiResponse<PaymentTransactionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePaymentTransaction(UpdatePaymentTransactionDto dto)
        {
            var result = await _paymenttransactionService.UpdatePaymentTransaction(dto);
            if (result == null)
            {
                return new RawJsonActionResult(
                    _jsonFieldsSerializer.Serialize(
                        new ApiResponse(false, "failed update", StatusCodes.Status400BadRequest),
                        string.Empty));
            }

            return new RawJsonActionResult(
                _jsonFieldsSerializer.Serialize(
                    new ApiResponse(true, "updated successfully", StatusCodes.Status200OK, result),
                    string.Empty));
        }



        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PaymentTransactionDto>),StatusCodes.Status200OK)]

        public async Task<IActionResult> GetTransactionForPayment(BaseDto<int> dto )
        { 
            var result = await _paymenttransactionService.GetPaymentTransactionForPayment(dto);
            if (result == null)
            {
                return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(false,"failed" ,StatusCodes.Status400BadRequest),string.Empty));
            }

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true ,"success", StatusCodes.Status200OK , result),string.Empty));

        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PaymentTransactionDto>),StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByPaymentMethod(BaseDto<int> dto)
        {
         var result=   _paymenttransactionService.GetPaymentTransactionDtosByMethod(dto);
           
            if (result == null)
            {
                return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(false, "failed", StatusCodes.Status400BadRequest), string.Empty));
            }

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "success", StatusCodes.Status200OK, result), string.Empty));


        }
    }
}
