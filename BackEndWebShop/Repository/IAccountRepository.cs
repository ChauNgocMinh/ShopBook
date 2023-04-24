using BackEndWebShop.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackEndWebShop.Repository
{
    public interface IAccountRepository
    {
        public Task<string> SignInAsync(SignInModel model);
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<IdentityUser> ShowUserAsync();
        public Task<IdentityUser> GetUserByEmailAsync(string EmailUser);
        public Task<IdentityUser> GetUserByNameAsync(string NameUser);
        public Task<IdentityUser> LockUserByEmailAsync(string IdUser);
        public Task<IdentityUser> UnlockUserByEmailAsync(string IdUser);
    }
}
