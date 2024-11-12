using Admission.Core.Domain.IdentityEntities;
using Admission.Core.DTO;
using Admission.Core.Enums;
using Admission.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Admission.UI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        [HttpGet]
        [Authorize("NotAuthorized")]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [Authorize("NotAuthorized")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            //Check for validation errors
            if (ModelState.IsValid == false)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(registerDTO);
            }

            ApplicationUser user = new ApplicationUser() { Email = registerDTO.Email, PhoneNumber = registerDTO.Phone, UserName = registerDTO.Email, PersonName = registerDTO.PersonName };

            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (result.Succeeded)
            {
                //Check status of radio button
                if (registerDTO.UserType == Core.Enums.UserTypeOptions.Admin)
                {
                    //Create 'Admin' role
                    if (await _roleManager.FindByNameAsync(UserTypeOptions.Admin.ToString()) is null)
                    {
                        ApplicationRole applicationRole = new ApplicationRole() { Name = UserTypeOptions.Admin.ToString() };
                        await _roleManager.CreateAsync(applicationRole);
                    }

                    //Add the new user into 'Admin' role
                    await _userManager.AddToRoleAsync(user, UserTypeOptions.Admin.ToString());
                }
                else
                {
                    //Create 'Admin' role
                    if (await _roleManager.FindByNameAsync(UserTypeOptions.User.ToString()) is null)
                    {
                        ApplicationRole applicationRole = new ApplicationRole() { Name = UserTypeOptions.User.ToString() };
                        await _roleManager.CreateAsync(applicationRole);
                    }

                    //Add the new user into 'User' role
                    await _userManager.AddToRoleAsync(user, UserTypeOptions.User.ToString());
                }

				// Thêm claim cho AccountID
				// Add Claim for AccountID
				var accountId = user.Id; // Lấy ID của người dùng
				await _userManager.AddClaimAsync(user, new Claim("AccountID", accountId.ToString())); // Chuyển đổi thành string
																									  //Sign in
				await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("Register", error.Description);
                }

                return View(registerDTO);
            }

        }


        [HttpGet]
        [Authorize("NotAuthorized")]
        
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [Authorize("NotAuthorized")]
        public async Task<IActionResult> Login(LoginDTO loginDTO, string? ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(loginDTO);
            }

            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                //Admin
                ApplicationUser user = await _userManager.FindByEmailAsync(loginDTO.Email);
                if (user != null)
                {

                    if (await _userManager.IsInRoleAsync(user, UserTypeOptions.Admin.ToString()))
                    {
                        return RedirectToAction("Index", "Home"/*, new { area = "Admin" }*/);
                    }
                }

                if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                {
                    return LocalRedirect(ReturnUrl);
                }
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            ModelState.AddModelError("Login", "Tài khoản hoặc mật khẩu không đúng!");
            ViewBag.Errors = "Tài khoản hoặc mật khẩu không đúng!";
            return View(loginDTO);
        }


        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        [AllowAnonymous]
        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true); //valid
            }
            else
            {
                return Json(false); //invalid
            }
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(changePasswordDTO);
            }

            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var result = await _userManager.ChangePasswordAsync(user, changePasswordDTO.CurrentPassword, changePasswordDTO.NewPassword);

            if (result.Succeeded)
            {
                //Sign the user out so they can log in with the new password
                await _signInManager.SignOutAsync();
                TempData["SuccessMessage"] = "Mật khẩu đã được thay đổi thành công, vui lòng đăng nhập lại.";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(changePasswordDTO);
        }
    }
}
