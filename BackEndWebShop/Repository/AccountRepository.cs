using BackEndWebShop.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BackEndWebShop.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace BackEndWebShop.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        public async Task<IdentityUser> GetUserByEmail(string IdUser)
        {
            var User = await userManager.FindByEmailAsync(IdUser);
            return User;
        }

        public Task<IdentityUser> GetUserByEmailAsync(string EmailUser)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> GetUserByNameAsync(string NameUser)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> LockUserByEmailAsync(string IdUser)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> ShowUserAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<string> SignInAsync(SignInModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (!result.Succeeded)
            {
                return string.Empty;
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
       
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
            };
            await userManager.CreateAsync(user, model.Password);
            return await userManager.AddToRoleAsync(user, "User");
        }

        public Task<IdentityUser> UnlockUserByEmailAsync(string IdUser)
        {
            throw new NotImplementedException();
        }
    }
}
