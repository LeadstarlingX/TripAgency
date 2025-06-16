using Microsoft.AspNetCore.Mvc;
using Application.IApplicationServices.Contact;
using Application.DTOs.Contact;
using Microsoft.AspNetCore.Authorization;
using Domain.Common;
using Application.Common;

namespace BlazorPresentation.Areas.Admin
{
    [Area(ApiConsts.AdminRoleName)]
    [Route("api/[area]/[controller]")]
    //[Authorize(Roles = ApiConsts.AdminRoleName)]
    public class ContactTypeController : ControllerBase
    {
        private readonly IContactTypeService _contactService;

        public ContactTypeController(IContactTypeService contactService)
        {
            _contactService = contactService;
        }

        #region GET: api/contacttype
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contactTypes = await _contactService.GetContactTypesAsync();
            return Ok(contactTypes);
        }
        #endregion

        #region POST: api/contacttype
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateContactTypeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _contactService.CreateContactAsync(dto);
            return CreatedAtAction(nameof(GetAll), result);
        }
        #endregion

        #region PUT: api/contacttype
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateContactTypeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _contactService.UpdateContactAsync(dto);
            return NoContent();
        }
        #endregion

        #region DELETE: api/contacttype
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] BaseDto<int> dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _contactService.DeleteContactAsync(dto);
            return NoContent();
        }
        #endregion
    }
}