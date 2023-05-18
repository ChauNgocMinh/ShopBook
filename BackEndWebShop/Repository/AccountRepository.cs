using BackEndWebShop.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BackEndWebShop.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace BackEndWebShop.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, BookShopContext _context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string EmailUser)
        {
            var User = await userManager.FindByEmailAsync(EmailUser);
            return User;
        }

        public async Task<IdentityResult> LockUserByEmailAsync(string UserName)
        {
            var user = await userManager.FindByEmailAsync(UserName);

            var lockoutEnd = DateTimeOffset.UtcNow.AddYears(100); // Lockout user for 100 years

            return await userManager.SetLockoutEndDateAsync(user, lockoutEnd);
        }

        public async Task<IdentityResult> UnlockUserByEmailAsync(string UserName)
        {
            var user = await userManager.FindByEmailAsync(UserName);

            return await userManager.SetLockoutEndDateAsync(user, null);
        }

        public async Task<List<string>> ShowUserAsync()
        {
            var user = await userManager.Users.ToListAsync();
            var ListUser = user.Select(u => u.UserName).ToList();
            return ListUser;
        }

        public async Task<string> SignInAsync(SignInModel model)
        {

            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            var user = await userManager.FindByEmailAsync(model.Email);
            
            var roles = await userManager.GetRolesAsync(user);

            // Nếu result không Succeeded thì return về chuỗi rỗng
            if (!result.Succeeded)
            {
                return string.Empty;
            }
            //Cấu hình Claim

            var authClaims = new List<Claim>
            {
/*                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
*/            };
            //Thêm Email vào claim
            authClaims.Add(new Claim("Email", model.Email));
            // Thêm toàn bộ role của user vào Claim
            foreach (var role in roles)
            {
                authClaims.Add(new Claim("role", role));
            }

            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            // Cấu hình chuỗi token
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );
            // Trả về chuỗi token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                IsAdmin = false,
            };
            await userManager.CreateAsync(user, model.Password);

            return await userManager.AddToRoleAsync(user, "User");
        }

     
    }
}
