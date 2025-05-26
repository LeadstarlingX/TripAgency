//using Application.DTOs.Authentication;
//using Application.IApplicationServices.Authentication;
//using Domain.Enum;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;

//namespace BlazorPresentation.Controllers
//{
//    [Route("/[controller]/[action]")]
//    public class AuthController : Controller
//    {
//        private readonly IAuthenticationService _authService;

//        public AuthController(IAuthenticationService authService)
//        {
//            _authService = authService;
//        }

//        [HttpGet]
//        public IActionResult Index()
//        {
//            return View();
//        }

//        [HttpGet]
//        public IActionResult Login()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Login(LoginDto loginDto)
//        {
//            if (!ModelState.IsValid)
//                return View(loginDto);

//            try
//            {
//                var user = await _authService.LoginAsync(loginDto);
//                return View("Index",user);
//            }
//            catch (Exception ex)
//            {
//                ModelState.AddModelError("", ex.Message);
//                return View(loginDto);
//            }
//        }

//        [HttpGet]
//        public IActionResult Register()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Register(RegisterDto registerDto)
//        {
//            if (!ModelState.IsValid)
//                return View(registerDto);

//            try
//            {
//                var result = await _authService.RegisterAsync(registerDto);

//                if (result.Succeeded)
//                {
//                    TempData["SuccessMessage"] = "Registration successful! Please login.";
//                    return RedirectToAction(nameof(Login));
//                }

//                foreach (var error in result.Errors)
//                {
//                    ModelState.AddModelError("", error.Description);
//                }
//            }
//            catch (Exception ex)
//            {
//                ModelState.AddModelError("", $"Registration failed: {ex.Message}");
//            }

//            return View(registerDto);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Logout()
//        {
//            await _authService.LogoutAsync();
//            return RedirectToAction("Index");
//        }
//    }
//}
