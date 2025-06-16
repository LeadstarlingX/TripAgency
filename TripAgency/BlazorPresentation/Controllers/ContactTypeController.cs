using Microsoft.AspNetCore.Mvc;
using Application.IApplicationServices.Contact;
using Application.DTOs.Contact;
using Microsoft.AspNetCore.Authorization;
using Domain.Common;
using Application.Common;

namespace Presentation.Controllers
{
    [Route("/[controller]/[action]")]
    [Authorize(Roles = ApiConsts.AdminRoleName)]
    public class ContactTypeController : Controller
    {
        private readonly IContactTypeService _contactServices;

        public ContactTypeController(IContactTypeService contactServices)
        {
            _contactServices = contactServices;
        }

        #region Get

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var contactTypes =await _contactServices.GetContactTypesAsync(); // Note: Method name might be misleading
            return View(contactTypes);
        }
        #endregion

        #region Create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateContactTypeDto createContactTypeDto)
        {
            if (ModelState.IsValid)
            {
                await _contactServices.CreateContactAsync(createContactTypeDto);
                return RedirectToAction(nameof(Index));
            }
            return View(createContactTypeDto);
        }
        #endregion

        #region Edit
        public IActionResult Edit(int id)
        {
            var contactTypeToEdit = new UpdateContactTypeDto { Id = id };
            return View(contactTypeToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UpdateContactTypeDto updateContactTypeDto)
        {
            if (id != updateContactTypeDto.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _contactServices.UpdateContactAsync(updateContactTypeDto);
                return RedirectToAction(nameof(Index));
            }
            return View(updateContactTypeDto);
        }

        #endregion

        #region Delete

        public IActionResult Delete(int id)
        {
            var contactTypeToDelete = new BaseDto<int> { Id = id };
            return View(contactTypeToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var baseDto = new BaseDto<int> { Id = id };
            await _contactServices.DeleteContactAsync(baseDto);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
