﻿using BackEndWebShop.Data;
using BackEndWebShop.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackEndWebShop.Repository
{
    public interface IAccountRepository
    {
        public Task<string> SignInAsync(SignInModel model);
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<List<string>> ShowUserAsync();
        public Task<ApplicationUser> GetUserByEmailAsync(string EmailUser);
        public Task<IdentityResult> LockUserByEmailAsync(string UserName);
        public Task<IdentityResult> UnlockUserByEmailAsync(string UserName);
        public Task<IdentityResult> ConfirmAccountAsync(string email);
        public Task<IdentityResult> RestPassAsync(string email, string newPass);
        public Task<IdentityResult> ChangePass(string email, string NewPass, string CurrentPass);
    }
}
