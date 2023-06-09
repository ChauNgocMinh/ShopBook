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
using System.Net;
using Microsoft.Ajax.Utilities;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System;
using Microsoft.AspNetCore.Authentication;

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
            sendEmail.ActivateAccount(signUpModel.Email);
            var result = await accountRepo.SignUpAsync(signUpModel);
            if (result.Succeeded)
            {
                return Ok("Vui lòng xác nhận email để thực hiện");
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Confirm(string Email)
        {
            await accountRepo.ConfirmAccountAsync(Email);
            return Ok("Tài khoản được tạo thành công!!!");
          
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ShowInfo()
        {
            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            var user = await _userManager.FindByEmailAsync(userEmail);
            return Ok(user);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ChagePass(string NowPass, string newPass, string ConfirmPassword)
        {
            var EmailUser = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            var CurrentPass = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Pass")?.Value;
            if(NowPass == CurrentPass && newPass == ConfirmPassword)
            {
                await accountRepo.ChangePass(EmailUser, newPass, CurrentPass);
                return Ok("Đổi mật khẩu thành công");
            }
            return BadRequest("Thông tin không chính xác");
        }

        [HttpGet]
        public async Task<IActionResult> RestPass(string Email)
        {
            sendEmail.ChagePassWord(Email);
            return Ok("Vui lòng xác nhận email để thực hiện");
        }

        [HttpGet]
        public async Task<IActionResult> ConfiPassword(string Email)
        {
            string NewPass = "123Aa!";
            try
            {
/*                var user = await _userManager.FindByEmailAsync(Email);
*/
                await accountRepo.RestPassAsync(Email, NewPass);

                return Ok("Mật khẩu mới: 123Aa!");
            }
            catch(Exception ex)
            { 
                return BadRequest(ex.ToString());
            }
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
