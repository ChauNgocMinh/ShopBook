﻿using BackEndWebShop.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackEndWebShop.Repository
{
    public interface IAccountRepository
    {
        public Task<string> SignInAsync(SignInModel model);
        public Task<IdentityResult> SignUpAsync(SignUpModel model);

    }
}