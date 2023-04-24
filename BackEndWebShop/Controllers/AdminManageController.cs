using BackEndWebShop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndWebShop.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    [Route("api/[action]")]
    public class AdminManageController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;

        private AdminManageController(UserManager<ApplicationUser> _userManager)
        {
            userManager = _userManager;
        }
        [HttpPut]
        public async Task<IActionResult> LockUser()
        {
            return Ok();
        }
    }
}
