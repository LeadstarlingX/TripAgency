using Microsoft.AspNetCore.Mvc;
using Application.IApplicationServices.Contact;
using Application.DTOs.Contact;
using Application.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using Domain.Common;
using System.Net;
using Application.Serializer;
using Application.DTOs.Actions;

namespace API.Areas.Admin
{
    [Area(ApiConsts.AdminRoleName)]
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = ApiConsts.AdminRoleName)]
    public class ContactTypeController : ControllerBase
    {
        private readonly IContactTypeService _contactService;
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;

        public ContactTypeController(IContactTypeService contactService, IJsonFieldsSerializer jsonFieldsSerializer)
        {
            _contactService = contactService;
            _jsonFieldsSerializer = jsonFieldsSerializer;
        }

        #region GET: api/Admin/ContactType/GetAll
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<ContactTypesDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var contactTypes = await _contactService.GetContactTypesAsync();
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, contactTypes), string.Empty));
        }
        #endregion

        #region POST: api/Admin/ContactType/Create
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<ContactTypeDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateContactTypeDto dto)
        {           
           var result = await _contactService.CreateContactAsync(dto);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "ContactType created successfully", StatusCodes.Status201Created, result), string.Empty)); 
        }
        #endregion

        #region PUT: api/Admin/ContactType/Update
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] UpdateContactTypeDto dto)
        {
            await _contactService.UpdateContactAsync(dto);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "ContactType updated successfuly", StatusCodes.Status200OK, null), string.Empty));    
        }
        #endregion

        #region DELETE: api/Admin/ContactType/Delete
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromBody] BaseDto<int> dto)
        {
            await _contactService.DeleteContactAsync(dto);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "ContactType deleted successfuly", StatusCodes.Status200OK, null), string.Empty));
        }
        #endregion
    }
}