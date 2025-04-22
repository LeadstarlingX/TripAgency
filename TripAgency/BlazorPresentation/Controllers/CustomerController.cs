using Microsoft.AspNetCore.Mvc;
using Application.DTOs.Customer;
using Application.IApplicationServices.Customer;
using Application.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using Domain.Common;

namespace WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    public class CustomerController : Controller 
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [Authorize(Roles = DefaultSetting.AdminRoleName)]
        public async Task<IActionResult> Index()
        {
            var customers = await _customerService.GetCustomersAsync();
            return View(customers); 
        }

        [HttpGet]
        [Authorize(DefaultSetting.AdminRoleName)]
        public IActionResult Create()
        {
            return RedirectToAction("Register", "Auth");
        }


        

        // GET: /Customers/UpdateCustomer/1
        //[HttpGet("{id}")]
        //public async Task<IActionResult> UpdateCustomer(long id)
        //{
        //    var customer = await _customerService.GetCustomerByIdAsync(id); // Add this method to your service
        //    if (customer == null)
        //        return NotFound();

        //    return View(customer);
        //}

        // POST: /Customers/UpdateCustomer
        //[HttpPost]
        //public async Task<IActionResult> UpdateCustomer(UpdateCustomerDto dto)
        //{
        //    if (!ModelState.IsValid)
        //        return View(dto);

        //    await _customerService.UpdateCustomerAsync(dto);
        //    return RedirectToAction(nameof(GetCustomers));
        //}

        //// POST: /Customers/DeleteCustomer/1
        //[HttpPost("{id}")]
        //public async Task<IActionResult> DeleteCustomer(long id)
        //{
        //    await _customerService.DeleteCustomerAsync(new BaseDto<long> { Id = id });
        //    return RedirectToAction(nameof(GetCustomers));
        //}
    }
}