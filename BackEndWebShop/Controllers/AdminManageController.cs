using BackEndWebShop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndWebShop.Controllers
{
    [Route("api/[action]")]
    public class AdminManageController : ControllerBase
    {
      /*  private readonly UserManager<ApplicationUser> userManager;

        private AdminManageController(UserManager<ApplicationUser> _userManager)
        {
            userManager = _userManager;
        }*/
        [Authorize(Roles = "Admin")]

        [HttpGet]
        public async Task<IActionResult> LockUser()
        {
            return Ok();
        }
    }
}
