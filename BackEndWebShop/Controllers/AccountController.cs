using BackEndWebShop.Model;
using BackEndWebShop.Helper;
using BackEndWebShop.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BackEndWebShop.Data;
using System.Text;
using System.Web;
/*using Microsoft.AspNet.Identity;
*/using Newtonsoft.Json;
using System.Net.Mail;

namespace BackEndWebShop.Controllers
{
    [Route("Controller/[action]")]

    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository accountRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SendEmail sendEmail = new SendEmail();
        public AccountController(IAccountRepository repo, UserManager<ApplicationUser> userManager)
        {
            accountRepo = repo;
            _userManager = userManager;

        }

        

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel signUpModel)
        {
            sendEmail.ConfirmationMail(signUpModel.Email);
            var result = await accountRepo.SignUpAsync(signUpModel);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Confirm(string Email)
        {
           
            var user = await accountRepo.GetUserByEmailAsync(Email);
            user.EmailConfirmed = true;
            
            return Ok(await _userManager.UpdateAsync(user));
            /* if (user == null)
             {
                 // Người dùng không tồn tại
                 return BadRequest();
             }
             var isConfirmed = await _userManager.IsEmailConfirmedAsync(user);
             if (isConfirmed)
             {
                 return Ok("Email đã được xác nhận");
             }
             else
             {
                 return Ok("Email chưa được xác nhận");
             }*/
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            var result = await accountRepo.SignInAsync(signInModel);

            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }

            return Ok(result);
        }
        /*[HttpPost]
        public async Task<IActionResult> Test(string email)
        {
            sendEmail.ValidationCode(email);
            return Ok();
        }*/

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ShowInfo()
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin(string EmailUser)
        {
            var User = await _userManager.FindByEmailAsync(EmailUser);
            var result = await _userManager.AddToRoleAsync(User, "Admin");
            if (result.Succeeded)
            {
                await _userManager.RemoveFromRoleAsync(User, "User");
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
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
