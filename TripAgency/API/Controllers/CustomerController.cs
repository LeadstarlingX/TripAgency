using Application.DTOs.Actions;
using Application.DTOs.Category;
using Application.DTOs.Common;
using Application.DTOs.Contact;
using Application.DTOs.Customer;
using Application.IApplicationServices.Authentication;
using Application.IApplicationServices.Customer;
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
    public class CustomerController : BaseAuthenticatedController
    {
        private readonly ICustomerService _customerService;
        public CustomerController(
            IAuthenticationService authenticationService,
            IJsonFieldsSerializer jsonFieldsSerializer,
            ICustomerService customerService) : base(authenticationService, jsonFieldsSerializer)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<CustomersDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCustomersAsync()
        {
            var result = await _customerService.GetCustomersAsync();
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<ContactsDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCustomerContactsAsync([FromQuery] BaseDto<long> dto)
        {
            var result = (await _customerService.GetCustomerContactAsync(dto));
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomerDto createCustomerDto)
        {
            createCustomerDto.Id = await _authenticationService.RegisterAsync(createCustomerDto.UserDto);
            var result = await _customerService.CreateCustomerAsync(createCustomerDto);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "Customer created successfuly", StatusCodes.Status201Created, result), string.Empty));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCustomerAsync([FromBody] UpdateCustomerDto updateCustomerDto)
        {
            var result = await _customerService.UpdateCustomerAsync(updateCustomerDto);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "Customer updated successfuly", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCustomerAsync([FromBody] BaseDto<long> dto)
        {
            await _customerService.DeleteCustomerAsync(dto);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "Customer deleted successfuly", StatusCodes.Status200OK), string.Empty));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCustomerContactAsync([FromBody] CreateCustomerContactDto dto)
        {
            await _customerService.CreateCustomerContactAsync(dto);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "Contact created successfully", StatusCodes.Status201Created), string.Empty));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCustomerContactAsync([FromBody] UpdateCustomerContactDto dto)
        {
            await _customerService.UpdateCustomerContactAsync(dto);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "Contact updated successfully", StatusCodes.Status200OK), string.Empty));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCustomerContactAsync([FromQuery] BaseDto<int> dto)
        {
            await _customerService.DeleteCustomerContactAsync(dto);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "Contact deleted successfully", StatusCodes.Status200OK), string.Empty));
        }
    }
}
