using BackEndWebShop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BackEndWebShop.Repository;

namespace BackEndWebShop.Controllers
{
    [Route("Controller/[action]")]
    [Authorize(Roles = "Admin")]
    public class AdminManageController : ControllerBase
    {
        private readonly IAccountRepository accountRepo;

        public AdminManageController(IAccountRepository _accountRepo)
        {
            accountRepo = _accountRepo;
        }
        [HttpGet]
        public async Task<IActionResult> ShowAllUser()
        {
            return Ok(await accountRepo.ShowUserAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetUserByEmail(string Email)
        {
            return Ok(await accountRepo.GetUserByEmailAsync(Email));
        }

        [HttpPost]
        public async Task<IActionResult> LockUser(string Email)
        {
            await accountRepo.LockUserByEmailAsync(Email);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UnLockUser(string Email)
        {
            await accountRepo.UnlockUserByEmailAsync(Email);
            return Ok();
        }
    }
}
