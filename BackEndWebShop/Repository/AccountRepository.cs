using BackEndWebShop.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BackEndWebShop.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IdentityUser> GetUserByEmailAsync(string EmailUser)
        {
            var User = await userManager.FindByEmailAsync(EmailUser);
            return User;
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
            var user = await userManager.FindByEmailAsync(model.Email);
            var roles = await userManager.GetRolesAsync(user);
          /*  Console.WriteLine(Roles);
            Console.Write(user);*/

            if (!result.Succeeded)
            {
                return string.Empty;
            }



            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            foreach (var role in roles)
            {
                authClaims.Add(new Claim("role", role));
            }

            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );

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

        public Task<IdentityUser> UnlockUserByEmailAsync(string IdUser)
        {
            throw new NotImplementedException();
        }
    }
}
