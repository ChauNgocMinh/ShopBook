using BackEndWebShop.Model;
using BackEndWebShop.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BackEndWebShop.Data;
using System.Security.Claims;

namespace BackEndWebShop.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository accountRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(IAccountRepository repo, UserManager<ApplicationUser> userManager)
        {
            accountRepo = repo;
            _userManager = userManager;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel signUpModel)
        {
            var result = await accountRepo.SignUpAsync(signUpModel);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }

            return Unauthorized();
        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            var result = await accountRepo.SignInAsync(signInModel);

            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }

            return Ok(result);
        }
        [HttpPut("AddAdmin")]
        public async Task<IActionResult> AddAdmin(string EmailUser)
        {
            var User = await _userManager.FindByEmailAsync(EmailUser);
            var result = await _userManager.AddToRoleAsync(User, "Admin");
            if (result.Succeeded)
            {
                return Ok(await _userManager.RemoveFromRoleAsync(User, "User"));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("RemoveAdmin")]
        public async Task<IActionResult> RemoveAdmin(string EmailUser)
        {
            var User = await _userManager.FindByEmailAsync(EmailUser);
            var result = await _userManager.AddToRoleAsync(User, "User");
            if (result.Succeeded)
            {
                return Ok(await _userManager.RemoveFromRoleAsync(User, "Admin"));
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
